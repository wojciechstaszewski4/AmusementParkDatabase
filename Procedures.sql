USE AmusementParkDB;
GO

-- PROCEDURES

-- Procedura anulowania zamowienia:

CREATE PROCEDURE dbo.CancelOrder
    @OrderID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Orders
        SET Status = 'Cancelled'
        WHERE ID = @OrderID AND Status NOT IN ('Cancelled', 'Returned', 'Shipped', 'Delivered');

        UPDATE Store_Inventory
        SET Quantity_In_Stock = Quantity_In_Stock + oi.Quantity
        FROM Store_Inventory
        INNER JOIN Order_Items oi ON Store_Inventory.ID_Products = oi.ID_Products
        WHERE oi.ID_Orders = @OrderID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

--------------------- TEST ---------------------
-- Sprawdzenie liczby produktow w zamowieniu z OrderID = 8:
SELECT ID_Products, Quantity 
FROM dbo.Order_Items 
WHERE ID_Orders = 8;
GO

-- Stan zamowienia i magazynu:
SELECT * FROM Orders WHERE ID = 8;
GO

SELECT * FROM Store_Inventory WHERE ID_Products IN (SELECT ID_Products FROM Order_Items WHERE ID_Orders = 8);
GO

-- Wykonanie procedury:
EXEC dbo.CancelOrder @OrderID = 8;
GO

-- Stan zamowienia i magazynu po anulowaniu:
SELECT * FROM Orders WHERE ID = 8;
GO

SELECT * FROM Store_Inventory WHERE ID_Products IN (SELECT ID_Products FROM Order_Items WHERE ID_Orders = 8);
GO
------------------------------------------------

-- Procedura aktualizacji cen produktow w oparciu o sklep:

CREATE PROCEDURE dbo.UpdateProductPricesByStore
    @StoreName NVARCHAR(50),
    @PercentageChange DECIMAL(5, 2)
AS
BEGIN
    BEGIN TRY
        UPDATE Products
        SET Price = Price * (1 + @PercentageChange / 100)
        WHERE ID IN (
            SELECT p.ID
            FROM Products p
            INNER JOIN Store_Inventory si ON p.ID = si.ID_Products
            INNER JOIN Stores s ON si.ID_Stores = s.ID
            WHERE s.Name = @StoreName
        );
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

--------------------- TEST ---------------------
-- Ceny produktow:
SELECT * FROM Products;
GO

-- Wykonanie procedury:
EXEC dbo.UpdateProductPricesByStore @StoreName = 'Sklep Pamiatkowy', @PercentageChange = 10;
GO

-- Ceny produktow po aktualizacji:
SELECT * FROM Products;
GO
------------------------------------------------

-- Procedura przypisania pracownika do atrakcji:

CREATE PROCEDURE dbo.AssignEmployeeToAttraction
    @EmployeeID INT,
    @AttractionID INT
AS
BEGIN
    BEGIN TRY
        UPDATE Attractions
        SET ID_Employees = @EmployeeID
        WHERE ID = @AttractionID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

--------------------- TEST ---------------------
-- Stan atrakcji dla AttractionID = 1:
SELECT * FROM Attractions WHERE ID = 2;
GO

-- Wykonanie procedury:
EXEC dbo.AssignEmployeeToAttraction @EmployeeID = 7, @AttractionID = 2;
GO

-- Stan atrakcji po przypisaniu pracownika:
SELECT * FROM Attractions WHERE ID = 2;
GO
------------------------------------------------