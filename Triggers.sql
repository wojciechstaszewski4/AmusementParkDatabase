USE AmusementParkDB;
GO

-- TRIGGERS

-- Trigger aktualizujacy stan magazynowy po dodaniu zamowienia:

CREATE TRIGGER dbo.UpdateInventoryAfterOrder
ON Order_Items
AFTER INSERT
AS
BEGIN
    UPDATE Store_Inventory
    SET Quantity_In_Stock = Quantity_In_Stock - inserted.Quantity
    FROM Store_Inventory
    INNER JOIN inserted ON Store_Inventory.ID_Products = inserted.ID_Products
    WHERE Store_Inventory.Quantity_In_Stock >= inserted.Quantity AND inserted.ID_Products IS NOT NULL;

    IF EXISTS (
        SELECT 1
        FROM Store_Inventory
        INNER JOIN inserted ON Store_Inventory.ID_Products = inserted.ID_Products
        WHERE Store_Inventory.Quantity_In_Stock < 0 AND inserted.ID_Products IS NOT NULL
    )
    BEGIN
        RAISERROR ('Nie mozna zrealizowac zamowienia, brak wystarczajacej ilosci produktow na stanie!', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;
GO

--------------------- TEST ---------------------
-- Sprawdzenie stanu magazynu:
SELECT * FROM dbo.Store_Inventory;
GO

-- Wstawienie nowego zamowienia:

INSERT INTO dbo.Orders (ID_Users, Order_Date, Total_Price, Status)
VALUES (5, '2025-01-02 12:30:50', 64.95, 'Confirmed');
GO

INSERT INTO dbo.Order_Items (ID_Orders, ID_Products, Quantity, Unit_Price) 
VALUES (11, 1, 5, 12.99);
GO

-- Sprawdzenie stanu magazynu:
SELECT * FROM dbo.Store_Inventory;
GO
------------------------------------------------

-- Trigger aktualizujacy srednia ocene pracownika po dodaniu nowej opinii:

CREATE TRIGGER dbo.UpdateEmployeeRating
ON Reviews
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE Employees
    SET Rating = (
        SELECT AVG(Staff_Rating)
        FROM Reviews
        WHERE ID_Employees = inserted.ID_Employees AND Staff_Rating IS NOT NULL
    )
    FROM Employees
    INNER JOIN inserted ON Employees.ID = inserted.ID_Employees
    WHERE inserted.ID_Employees IS NOT NULL AND inserted.Staff_Rating IS NOT NULL;
END;
GO

--------------------- TEST ---------------------
-- Sprawdzenie ocen atrakcji:
SELECT ID, Rating FROM dbo.Employees;
GO

-- Wstawienie nowej opinii:
INSERT INTO dbo.Reviews (ID_Users, ID_Employees, Staff_Rating, Comment)
VALUES (1, 1, 4.2, 'Bardzo pomocny');
GO

-- Sprawdzenie ocen atrakcji:
SELECT ID, Rating FROM dbo.Employees;
GO
------------------------------------------------

-- Trigger zmieniajacy status biletu po uplywie daty wygasniecia:

CREATE TRIGGER dbo.UpdateTicketStatusAfterExpiry
ON Tickets
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE Tickets
    SET Status = 'Expired'
    WHERE Expiry_Date < GETDATE() AND Expiry_Date IS NOT NULL AND Status NOT IN ('Expired', 'Cancelled', 'Used');
END;
GO

--------------------- TEST ---------------------
-- Sprawdzenie statusu biletow:
SELECT Ticket_Number, Expiry_Date, Status FROM dbo.Tickets;
GO

-- Aktualizacja biletow:
UPDATE dbo.Tickets
SET Expiry_Date = DATEADD(DAY, -1, GETDATE())
WHERE ID = 2;
GO

-- Sprawdzenie statusu biletow:
SELECT Ticket_Number, Expiry_Date, Status FROM dbo.Tickets;
GO
------------------------------------------------