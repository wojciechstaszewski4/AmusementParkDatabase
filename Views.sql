USE AmusementParkDB;
GO

-- VIEWS

-- Widok aktywnych promocji:

CREATE VIEW dbo.ActivePromotions AS
SELECT 
    p.ID AS Promotion_ID,
    p.Name AS Promotion_Name,
    p.Description AS Promotion_Description,
    p.Start_Date,
    p.End_Date,
    p.Discount_Percentage,
    p.Discount_Amount,
    COALESCE(pr.Name, a.Name, e.Name) AS Item_Name,
    CASE 
        WHEN pr.ID IS NOT NULL THEN 'Product'
        WHEN a.ID IS NOT NULL THEN 'Attraction Ticket'
        WHEN e.ID IS NOT NULL THEN 'Event Ticket'
        ELSE 'Unknown'
    END AS Item_Type,
    p.Status
FROM dbo.Promotions p
LEFT JOIN dbo.Products pr ON p.ID_Products = pr.ID
LEFT JOIN dbo.Attractions a ON p.ID_Attractions = a.ID
LEFT JOIN dbo.Events e ON p.ID_Events = e.ID
WHERE p.Status = 'Active';
GO

--------------------- TEST ---------------------
SELECT * FROM dbo.ActivePromotions;
GO
------------------------------------------------

-- Widok niepotwierdzonych rezerwacji:

CREATE VIEW dbo.PendingReservations AS
SELECT 
    r.ID AS Reservation_ID,
    r.Reservation_Date,
    r.Status,
    r.Total_Cost,
    u.Name AS User_Name,
    u.Surname AS User_Surname,
    ISNULL(a.Name, e.Name) AS Reserved_Item,
    CASE 
        WHEN a.ID IS NOT NULL THEN 'Attraction'
        WHEN e.ID IS NOT NULL THEN 'Event'
        ELSE 'Unknown'
    END AS Item_Type
FROM dbo.Reservations r
LEFT JOIN dbo.Users u ON r.ID_Users = u.ID
LEFT JOIN dbo.Attractions a ON r.ID_Attractions = a.ID
LEFT JOIN dbo.Events e ON r.ID_Events = e.ID
WHERE r.Status = 'Pending';
GO

--------------------- TEST ---------------------
SELECT * FROM dbo.PendingReservations;
GO
------------------------------------------------

-- Widok zamowien uzytkownikow:

CREATE VIEW dbo.UserOrders AS
SELECT 
    o.ID AS Order_ID,
    o.Order_Date,
    o.Total_Price,
    o.Status,
    u.Name AS User_Name,
    u.Surname AS User_Surname,
    COALESCE(a.Name, e.Name, p.Name, 'Unknown') AS Item_Name,
    CASE
        WHEN oi.ID_Attractions IS NOT NULL THEN 'Attraction Ticket'
        WHEN oi.ID_Events IS NOT NULL THEN 'Event Ticket'
        WHEN oi.ID_Products IS NOT NULL THEN 'Product'
        ELSE 'Unknown'
    END AS Item_Type,
    oi.Quantity,
    oi.Unit_Price
FROM dbo.Orders o
LEFT JOIN dbo.Users u ON o.ID_Users = u.ID
INNER JOIN dbo.Order_Items oi ON o.ID = oi.ID_Orders
LEFT JOIN dbo.Attractions a ON oi.ID_Attractions = a.ID
LEFT JOIN dbo.Events e ON oi.ID_Events = e.ID
LEFT JOIN dbo.Products p ON oi.ID_Products = p.ID;
GO

--------------------- TEST ---------------------
SELECT * FROM dbo.UserOrders;
GO
------------------------------------------------