USE MASTER CREATE DATABASE AmusementParkDB;
GO

USE AmusementParkDB;
GO

-- CREATE TABLE:

-- Struktura tabeli dla tabeli `Users`:

CREATE TABLE Users (
        ID INT NOT NULL IDENTITY (1, 1),
        Name NVARCHAR (50) NOT NULL,
        Surname NVARCHAR (50) NOT NULL,
        Address NVARCHAR (255) NULL,
        Postal_Code NVARCHAR (10) NULL,
        City NVARCHAR (50) NULL,
        Phone_Number NVARCHAR (20) NULL,
        Email NVARCHAR (100) UNIQUE NULL,
        Birth_Date DATE NULL,
        Gender NVARCHAR (6) CHECK (Gender IN ('Male', 'Female', 'Other')) NULL,
        Login NVARCHAR (50) UNIQUE NULL,
        Password NVARCHAR (255) NOT NULL,
        Add_Date DATETIME NOT NULL DEFAULT GETDATE (),
        Last_Login_Date DATETIME NULL,
        Status NVARCHAR (8) CHECK (Status IN ('Active', 'Inactive')) NULL,
        CONSTRAINT PK_Users PRIMARY KEY (ID)
    );
GO

-- Struktura tabeli dla tabeli `ACL`:

CREATE TABLE ACL (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Users INT NOT NULL,
        ACL NVARCHAR (255) NOT NULL,
        CONSTRAINT PK_ACL PRIMARY KEY (ID),
        CONSTRAINT FK_ACL_Users FOREIGN KEY (ID_Users) REFERENCES Users (ID) ON DELETE CASCADE
    );
GO

-- Struktura tabeli dla tabeli `Agreements`:

CREATE TABLE Agreements (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Users INT NOT NULL,
        Type NVARCHAR (50) NOT NULL,
        Date DATE NOT NULL,
        CONSTRAINT PK_Agreements PRIMARY KEY (ID),
        CONSTRAINT FK_Agreements_Users FOREIGN KEY (ID_Users) REFERENCES Users (ID) ON DELETE CASCADE
    );
GO

-- Struktura tabeli dla tabeli `Attraction_Categories`:

CREATE TABLE Attraction_Categories (
        ID INT NOT NULL IDENTITY (1, 1),
        Name NVARCHAR (50) NOT NULL,
        Description NVARCHAR (255) NULL,
        CONSTRAINT PK_Attraction_Categories PRIMARY KEY (ID)
    );
GO

-- Struktura tabeli dla tabeli `Departments`:

CREATE TABLE Departments (
        ID INT NOT NULL IDENTITY (1, 1),
        Name NVARCHAR (50) NOT NULL UNIQUE,
        Description NVARCHAR (255) NULL,
        CONSTRAINT PK_Departments PRIMARY KEY (ID)
    );
GO

-- Struktura tabeli dla tabeli `Employees`:

CREATE TABLE Employees (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Users INT NOT NULL UNIQUE,
        ID_Departments INT NULL,
        Job_Title NVARCHAR (50) NULL,
        Salary DECIMAL(10, 2) NOT NULL,
        Employment_Date DATE NOT NULL,
        Termination_Date DATE NULL,
        Status NVARCHAR (10) CHECK (Status IN ('Active', 'Inactive', 'Resigned', 'Retired')) NULL,
        Emergency_Contact NVARCHAR (255) NULL,
        Rating DECIMAL(2, 1) NULL,
        Comment NVARCHAR (MAX) NULL,
        Date DATETIME NOT NULL DEFAULT GETDATE (),
        CONSTRAINT PK_Employees PRIMARY KEY (ID),
        CONSTRAINT FK_Employees_Users FOREIGN KEY (ID_Users) REFERENCES Users (ID) ON DELETE CASCADE,
        CONSTRAINT FK_Employees_Departments FOREIGN KEY (ID_Departments) REFERENCES Departments (ID) ON DELETE CASCADE
    );
GO

-- Struktura tabeli dla tabeli `Events`:

CREATE TABLE Events (
        ID INT NOT NULL IDENTITY (1, 1),
        Name NVARCHAR (50) NOT NULL,
        Description NVARCHAR (255) NULL,
        Start_Date DATETIME NOT NULL,
        End_Date DATETIME NOT NULL,
        Ticket_Price DECIMAL(10, 2) NULL,
        CONSTRAINT PK_Events PRIMARY KEY (ID)
    );
GO

-- Struktura tabeli dla tabeli `Attractions`:

CREATE TABLE Attractions (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Attraction_Categories INT NOT NULL,
        ID_Users INT NULL,
        ID_Employees INT NULL,
        ID_Events INT NULL,
        Name NVARCHAR (100) NOT NULL,
        Description NVARCHAR (255) NULL,
        Type NVARCHAR (50) NULL,
        Capacity INT NULL,
        Status NVARCHAR (50) CHECK (Status IN ('Active', 'Inactive', 'Under Maintenance')) NULL,
        Opening_Date DATE NULL,
        Closing_Date DATE NULL,
        Maintenance_Date DATE NULL,
        Ticket_Price DECIMAL(10, 2) NULL,
        Available_Slots INT NULL,
        Occupied_Slots INT NULL,
        Age_Restriction INT NULL,
        Supervisor NVARCHAR (50) NULL,
        Location NVARCHAR (255) NULL,
        CONSTRAINT PK_Attractions PRIMARY KEY (ID),
        CONSTRAINT FK_Attractions_Attraction_Categories FOREIGN KEY (ID_Attraction_Categories) REFERENCES Attraction_Categories (ID) ON DELETE CASCADE,
        CONSTRAINT FK_Attractions_Users FOREIGN KEY (ID_Users) REFERENCES Users (ID) ON DELETE SET NULL,
        CONSTRAINT FK_Attractions_Employees FOREIGN KEY (ID_Employees) REFERENCES Employees (ID) ON DELETE NO ACTION,
        CONSTRAINT FK_Attractions_Events FOREIGN KEY (ID_Events) REFERENCES Events (ID) ON DELETE SET NULL
    );
GO

-- Struktura tabeli dla tabeli `Stores`:

CREATE TABLE Stores (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Employees INT NULL,
        Name NVARCHAR (50) NOT NULL,
        Location NVARCHAR (255) NULL,
        Opening_Hours NVARCHAR (255) NULL,
        Contact_Information NVARCHAR (255) NULL,
        Supervisor NVARCHAR (50) NULL,
        Rating DECIMAL(2, 1) NULL,
        CONSTRAINT PK_Stores PRIMARY KEY (ID),
        CONSTRAINT FK_Stores_Employees FOREIGN KEY (ID_Employees) REFERENCES Employees (ID) ON DELETE SET NULL
    );
GO

-- Struktura tabeli dla tabeli `Coupons`:

CREATE TABLE Coupons (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Users INT NULL,
        ID_Stores INT NULL,
        Code NVARCHAR (20) UNIQUE NULL,
        Expiry_Date DATE NULL,
        Discount_Percentage DECIMAL(5, 2) NULL,
        Discount_Amount DECIMAL(10, 2) NULL,
        Single_Use NVARCHAR (3) CHECK (Single_Use IN ('Yes', 'No')) NULL,
        Multiple_Uses INT DEFAULT 1,
        Applicable_Attractions NVARCHAR (255) NULL,
        Applicable_Events NVARCHAR (255) NULL,
        CONSTRAINT PK_Coupons PRIMARY KEY (ID),
        CONSTRAINT FK_Coupons_Users FOREIGN KEY (ID_Users) REFERENCES Users (ID) ON DELETE CASCADE,
        CONSTRAINT FK_Coupons_Stores FOREIGN KEY (ID_Stores) REFERENCES Stores (ID) ON DELETE CASCADE
    );
GO

-- Struktura tabeli dla tabeli `Orders`:

CREATE TABLE Orders (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Users INT NULL,
        Order_Date DATETIME NOT NULL DEFAULT GETDATE (),
        Total_Price DECIMAL(10, 2) NOT NULL,
        Status NVARCHAR (10) CHECK (Status IN ('Pending', 'Confirmed', 'Cancelled', 'Shipped', 'Delivered', 'Returned' ) ) NULL,
        CONSTRAINT PK_Orders PRIMARY KEY (ID),
        CONSTRAINT FK_Orders_Users FOREIGN KEY (ID_Users) REFERENCES Users (ID) ON DELETE SET NULL
    );
GO

-- Struktura tabeli dla tabeli `Products`:

CREATE TABLE Products (
        ID INT NOT NULL IDENTITY (1, 1),
        Name NVARCHAR (50) NOT NULL,
        Description NVARCHAR (255) NULL,
        Price DECIMAL(10, 2) NOT NULL,
        Stock_Quantity INT NOT NULL,
        Status NVARCHAR (12) CHECK (Status IN ('Active', 'Inactive', 'Out of Stock')) NULL,
        Rating DECIMAL(2, 1) NULL,
        CONSTRAINT PK_Products PRIMARY KEY (ID)
    );
GO

-- Struktura tabeli dla tabeli `Order_Items`:

CREATE TABLE Order_Items (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Orders INT NOT NULL,
        ID_Attractions INT NULL,
        ID_Events INT NULL,
        ID_Products INT NULL,
        Quantity INT NOT NULL,
        Unit_Price DECIMAL(10, 2) NOT NULL,
        CONSTRAINT PK_Order_Items PRIMARY KEY (ID),
        CONSTRAINT FK_Order_Items_Orders FOREIGN KEY (ID_Orders) REFERENCES Orders (ID) ON DELETE CASCADE,
        CONSTRAINT FK_Order_Items_Attractions FOREIGN KEY (ID_Attractions) REFERENCES Attractions (ID) ON DELETE SET NULL,
        CONSTRAINT FK_Order_Items_Events FOREIGN KEY (ID_Events) REFERENCES Events (ID) ON DELETE CASCADE,
        CONSTRAINT FK_Order_Items_Products FOREIGN KEY (ID_Products) REFERENCES Products (ID) ON DELETE SET NULL
    );
GO

-- Struktura tabeli dla tabeli `Promotions`:

CREATE TABLE Promotions (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Attractions INT NULL,
        ID_Events INT NULL,
        ID_Products INT NULL,
        Name NVARCHAR (50) NOT NULL,
        Description NVARCHAR (255) NULL,
        Start_Date DATETIME NOT NULL,
        End_Date DATETIME NOT NULL,
        Discount_Percentage DECIMAL(5, 2) NULL,
        Discount_Amount DECIMAL(10, 2) NULL,
        Applicable_Attractions NVARCHAR (255) NULL,
        Applicable_Events NVARCHAR (255) NULL,
        Status NVARCHAR (10) CHECK (Status IN ('Active', 'Inactive', 'Expired')) NULL,
        CONSTRAINT PK_Promotions PRIMARY KEY (ID),
        CONSTRAINT FK_Promotions_Attractions FOREIGN KEY (ID_Attractions) REFERENCES Attractions (ID) ON DELETE CASCADE,
        CONSTRAINT FK_Promotions_Events FOREIGN KEY (ID_Events) REFERENCES Events (ID) ON DELETE CASCADE,
        CONSTRAINT FK_Promotions_Products FOREIGN KEY (ID_Products) REFERENCES Products (ID) ON DELETE CASCADE
    );
GO

-- Struktura tabeli dla tabeli `Reservations`:

CREATE TABLE Reservations (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Users INT NOT NULL,
        ID_Attractions INT NULL,
        ID_Events INT NULL,
        Reservation_Date DATE NOT NULL,
        Status NVARCHAR (10) CHECK (Status IN ('Pending', 'Confirmed', 'Cancelled')) NULL,
        Total_Cost DECIMAL(10, 2) NOT NULL,
        Number_Of_People INT NOT NULL,
        Special_Request NVARCHAR (255) NULL,
        Reservation_Code NVARCHAR (50) UNIQUE NULL,
        CONSTRAINT PK_Reservations PRIMARY KEY (ID),
        CONSTRAINT FK_Reservations_Users FOREIGN KEY (ID_Users) REFERENCES Users (ID) ON DELETE CASCADE,
        CONSTRAINT FK_Reservations_Attractions FOREIGN KEY (ID_Attractions) REFERENCES Attractions (ID) ON DELETE SET NULL,
        CONSTRAINT FK_Reservations_Events FOREIGN KEY (ID_Events) REFERENCES Events (ID) ON DELETE SET NULL
    );
GO

-- Struktura tabeli dla tabeli `Reviews`:

CREATE TABLE Reviews (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Users INT NULL,
        ID_Employees INT NULL,
        ID_Attractions INT NULL,
        ID_Products INT NULL,
        ID_Stores INT NULL,
        Rating DECIMAL(2, 1) NULL,
        Comment NVARCHAR (MAX) NULL,
        Date DATETIME NOT NULL DEFAULT GETDATE (),
        User_Rating DECIMAL(2, 1) NULL,
        Staff_Rating DECIMAL(2, 1) NULL,
        Cleanliness_Rating DECIMAL(2, 1) NULL,
        CONSTRAINT PK_Reviews PRIMARY KEY (ID),
        CONSTRAINT FK_Reviews_Users FOREIGN KEY (ID_Users) REFERENCES Users (ID) ON DELETE SET NULL,
        CONSTRAINT FK_Reviews_Employees FOREIGN KEY (ID_Employees) REFERENCES Employees (ID) ON DELETE NO ACTION,
        CONSTRAINT FK_Reviews_Attractions FOREIGN KEY (ID_Attractions) REFERENCES Attractions (ID) ON DELETE SET NULL,
        CONSTRAINT FK_Reviews_Products FOREIGN KEY (ID_Products) REFERENCES Products (ID) ON DELETE SET NULL,
        CONSTRAINT FK_Reviews_Stores FOREIGN KEY (ID_Stores) REFERENCES Stores (ID) ON DELETE CASCADE
    );
GO

-- Struktura tabeli dla tabeli `Store_Inventory`:

CREATE TABLE Store_Inventory (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Stores INT NOT NULL,
        ID_Products INT NOT NULL,
        Quantity_In_Stock INT DEFAULT 0,
        CONSTRAINT PK_Store_Inventory PRIMARY KEY (ID),
        CONSTRAINT FK_Store_Inventory_Stores FOREIGN KEY (ID_Stores) REFERENCES Stores (ID) ON DELETE CASCADE,
        CONSTRAINT FK_Store_Inventory_Products FOREIGN KEY (ID_Products) REFERENCES Products (ID) ON DELETE CASCADE
    );
GO

-- Struktura tabeli dla tabeli `Tickets`:

CREATE TABLE Tickets (
        ID INT NOT NULL IDENTITY (1, 1),
        ID_Users INT NOT NULL,
        ID_Attractions INT NULL,
        ID_Events INT NULL,
        Ticket_Number NVARCHAR (50) UNIQUE NULL,
        Purchase_Date DATE NOT NULL,
        Expiry_Date DATE NULL,
        Status NVARCHAR (10) CHECK (Status IN ('Active', 'Used', 'Expired', 'Cancelled')) NULL,
        Price DECIMAL(10, 2) NOT NULL,
        Ticket_Type NVARCHAR (50) NULL,
        CONSTRAINT PK_Tickets PRIMARY KEY (ID),
        CONSTRAINT FK_Tickets_Users FOREIGN KEY (ID_Users) REFERENCES Users (ID) ON DELETE CASCADE,
        CONSTRAINT FK_Tickets_Attractions FOREIGN KEY (ID_Attractions) REFERENCES Attractions (ID) ON DELETE CASCADE,
        CONSTRAINT FK_Tickets_Events FOREIGN KEY (ID_Events) REFERENCES Events (ID) ON DELETE CASCADE
    );
GO

-- INSERT INTO:

-- INSERT dla tabeli `Users`:

INSERT INTO
    Users (
        Name,
        Surname,
        Address,
        Postal_Code,
        City,
        Phone_Number,
        Email,
        Birth_Date,
        Gender,
        Login,
        Password,
        Add_Date,
        Last_Login_Date,
        Status
    )
VALUES
    (
        'Jan',
        'Kowalski',
        'ul. Wislana 45/65',
        '12-345',
        'Krakow',
        '123-456-789',
        'jan.kowalski@example.com',
        '1986-09-08',
        'Male',
        'janekkowalski',
        'haslo123',
        '2024-01-01 08:42:21',
        '2024-10-23 14:15:59',
        'Active'
    ),
    (
        'Anna',
        'Nowak',
        'ul. Kwiatowa 3',
        '54-321',
        'Warszawa',
        '987-654-321',
        'anna.nowak@example.com',
        '1998-05-15',
        'Female',
        'ankanowak',
        'haslo456',
        '2024-02-15 19:13:51',
        '2024-10-25 09:30:22',
        'Active'
    ),
    (
        'Piotr',
        'Wisniewski',
        'ul. Glowna 10',
        '00-001',
        'Poznan',
        '234-567-890',
        'piotr.wisniewski@example.com',
        '1992-04-02',
        'Male',
        'piotrw',
        'haslo789',
        '2024-03-20 10:25:30',
        '2024-10-20 16:45:11',
        'Active'
    ),
    (
        'Katarzyna',
        'Lewandowska',
        'ul. Kwiatowa 11',
        '43-500',
        'Bielsko-Biala',
        '345-678-901',
        'katarzyna.lewandowska@example.com',
        '1985-06-21',
        'Female',
        'katalew',
        'haslo101',
        '2024-04-01 14:40:05',
        '2024-10-18 10:05:30',
        'Active'
    ),
    (
        'Marek',
        'Mazur',
        'ul. Nowa 25',
        '12-345',
        'Krakow',
        '456-789-012',
        'marek.mazur@example.com',
        '1978-11-30',
        'Male',
        'marekm',
        'haslo121',
        '2024-04-15 15:20:45',
        '2024-10-22 13:11:55',
        'Inactive'
    ),
    (
        'Ewa',
        'Zielinska',
        'ul. Dluga 35',
        '67-890',
        'Szczecin',
        '567-890-123',
        'ewa.zielinska@example.com',
        '1995-08-17',
        'Female',
        'ewaz',
        'haslo131',
        '2024-05-05 16:25:30',
        '2024-10-20 18:05:20',
        'Active'
    ),
    (
        'Robert',
        'Nowak',
        'ul. Jasna 8',
        '12-123',
        'Gdansk',
        '678-901-234',
        'robert.nowak@example.com',
        '1980-02-05',
        'Male',
        'robertn',
        'haslo141',
        '2024-05-15 09:10:15',
        '2024-10-19 11:45:05',
        'Inactive'
    ),
    (
        'Magdalena',
        'Pawlowska',
        'ul. Lesna 27',
        '98-765',
        'Kielce',
        '789-012-345',
        'magdalena.pawlowska@example.com',
        '1989-10-25',
        'Female',
        'magdap',
        'haslo151',
        '2024-06-01 08:35:45',
        '2024-10-21 15:55:15',
        'Active'
    ),
    (
        'Adam',
        'Wojciechowski',
        'ul. Mila 3',
        '77-123',
        'Katowice',
        '890-123-456',
        'adam.wojciechowski@example.com',
        '1990-07-19',
        'Male',
        'adamw',
        'haslo161',
        '2024-06-25 09:15:50',
        '2024-10-17 09:55:35',
        'Active'
    ),
    (
        'Zofia',
        'Wisniewska',
        'ul. Wspolna 4',
        '11-234',
        'Wroclaw',
        '901-234-567',
        'zofia.wisniewska@example.com',
        '1988-03-15',
        'Female',
        'zofiw',
        'haslo171',
        '2024-07-01 12:10:25',
        '2024-10-23 16:35:45',
        'Inactive'
    );
GO

-- INSERT dla tabeli `ACL`:

INSERT INTO
    ACL (ID_Users, ACL)
VALUES
    (1, 'Pracownik'),
    (2, 'Admin'),
    (3, 'Uzytkownik'),
    (4, 'Pracownik'),
    (5, 'Uzytkownik'),
    (6, 'Uzytkownik'),
    (7, 'Pracownik'),
    (8, 'Moderator'),
    (9, 'Uzytkownik'),
    (10, 'Pracownik');
GO

-- INSERT dla tabeli `Agreements`:

INSERT INTO
    Agreements (ID_Users, Type, Date)
VALUES
    (1, 'Regulamin', '2024-01-15'),
    (2, 'Polityka Prywatnosci', '2024-02-15'),
    (3, 'Regulamin', '2024-03-20'),
    (4, 'Polityka Prywatnosci', '2024-04-05'),
    (5, 'Regulamin', '2024-04-16'),
    (6, 'Regulamin', '2024-05-08'),
    (7, 'Polityka Prywatnosci', '2024-05-15'),
    (8, 'Regulamin', '2024-06-02'),
    (9, 'Polityka Prywatnosci', '2024-06-26'),
    (10, 'Regulamin', '2024-07-01');
GO

-- INSERT dla tabeli `Attraction_Categories`:

INSERT INTO
    Attraction_Categories (Name, Description)
VALUES
    (
        'Ekstremalne',
        'Atrakcje dla milosnikow adrenaliny.'
    ),
    (
        'Rodzinne',
        'Atrakcje odpowiednie dla calej rodziny.'
    ),
    ('Wodne', 'Atrakcje wodne dla ochlody i rozrywki.'),
    (
        'Dla dzieci',
        'Atrakcje przeznaczone dla najmlodszych.'
    ),
    (
        'Przygodowe',
        'Przezyj przygode na swiezym powietrzu.'
    ),
    (
        'Relaksacyjne',
        'Atrakcje do odpoczynku i relaksu.'
    ),
    (
        'Sezonowe',
        'Atrakcje dostepne w okreslonym sezonie.'
    ),
    (
        'Interaktywne',
        'Atrakcje, ktore angazuja uczestnikow.'
    );
GO

-- INSERT dla tabeli `Departments`:

INSERT INTO
    Departments (Name, Description)
VALUES
    (
        'Obsluga Klienta',
        'Odpowiedzialny za obsluge klientow i gosci.'
    ),
    (
        'Gastronomia',
        'Odpowiedzialny za obsluge gastronomiczna.'
    ),
    (
        'Techniczne',
        'Zarzadza konserwacja i technika atrakcji.'
    ),
    (
        'Marketing',
        'Zarzadza promocjami i reklama parku.'
    ),
    ('Administracja', 'Dzial administracji parku.'),
    (
        'Bezpieczenstwo',
        'Odpowiedzialny za bezpieczenstwo w parku.'
    ),
    (
        'HR',
        'Zajmuje sie rekrutacja i zasobami ludzkimi.'
    ),
    ('Finanse', 'Zarzadza finansami parku.'),
    ('Kreatywny', 'Tworzy nowe atrakcje i wydarzenia.'),
    (
        'Logistyka',
        'Odpowiedzialny za logistyke i dostawy.'
    );
GO

-- INSERT dla tabeli `Employees`:

INSERT INTO
    Employees (
        ID_Users,
        ID_Departments,
        Job_Title,
        Salary,
        Employment_Date,
        Termination_Date,
        Status,
        Emergency_Contact,
        Rating,
        Comment,
        Date
    )
VALUES
    (
        1,
        3,
        'Technik',
        3500.00,
        '2024-01-15',
        NULL,
        'Active',
        'Partner: Anna Kowalska',
        5.0,
        'Specjalista w konserwacji atrakcji.',
        '2024-05-02 08:15:00'
    ),
    (
        2,
        2,
        'Kierownik Gastronomii',
        4800.99,
        '2024-02-20',
        NULL,
        'Active',
        'Rodzic: Jan Nowak',
        4.5,
        'Doskonale zarzadza zespolem gastronomii.',
        '2024-06-12 11:30:00'
    ),
    (
        3,
        1,
        'Pracownik obsługi klienta',
        4000.00,
        '2024-03-01',
        NULL,
        'Active',
        'Brat: Pawel Wojcik',
        4.7,
        'Bardzo pomocny i profesjonalny.',
        '2024-07-01 14:00:00'
    ),
    (
        4,
        3,
        'Technik',
        3900.00,
        '2024-02-01',
        NULL,
        'Active',
        'Siostra: Katarzyna Janik',
        4.6,
        'Pomyslowy i zaangazowany w prace.',
        '2024-05-20 10:25:00'
    ),
    (
        5,
        5,
        'Administrator',
        4200.00,
        '2024-03-10',
        NULL,
        'Active',
        'Zona: Monika Zielinska',
        5.0,
        'Dba o plynnosc pracy administracji.',
        '2024-06-15 12:00:00'
    ),
    (
        6,
        6,
        'Ochroniarz',
        3300.00,
        '2024-01-20',
        NULL,
        'Active',
        'Syn: Lukasz Kowalski',
        4.8,
        'Swietnie dba o bezpieczenstwo gosci.',
        '2024-05-15 15:45:00'
    ),
    (
        7,
        3,
        'Technik',
        3600.00,
        '2024-04-01',
        NULL,
        'Active',
        'Partner: Marta Lukowska',
        4.3,
        'Dba o atmosfere w pracy.',
        '2024-07-10 16:00:00'
    ),
    (
        8,
        8,
        'Analityk Finansowy',
        4600.00,
        '2024-05-01',
        NULL,
        'Active',
        'Partner: Adam Wojcik',
        4.9,
        'Znajomosc finansow na wysokim poziomie.',
        '2024-08-01 10:00:00'
    ),
    (
        9,
        9,
        'Tworca Atrakcji',
        4000.00,
        '2024-04-20',
        NULL,
        'Active',
        'Brat: Marek Dabrowski',
        NULL,
        'Zaskakuje nowymi pomyslami.',
        '2024-06-20 13:45:00'
    ),
    (
        10,
        3,
        'Technik',
        3400.00,
        '2024-03-15',
        NULL,
        'Active',
        'Partnerka: Ewelina Wojtas',
        NULL,
        'Specjalista w obsludze atrakcji.',
        '2024-05-25 14:15:00'
    );
GO

-- INSERT dla tabeli `Events`:

INSERT INTO
    Events (
        Name,
        Description,
        Start_Date,
        End_Date,
        Ticket_Price
    )
VALUES
    (
        'Halloween Event',
        'Przerazajacy wieczor pelen atrakcji!',
        '2024-10-31 17:00:00',
        '2024-10-31 23:00:00',
        30.00
    ),
    (
        'Dzien Rodziny',
        'Dzien pelen zabawy dla calych rodzin.',
        '2024-11-15 10:00:00',
        '2024-11-15 18:00:00',
        20.00
    ),
    (
        'Festiwal Wiosenny',
        'Powitanie wiosny w parku!',
        '2024-03-20 09:00:00',
        '2024-03-20 20:00:00',
        15.00
    ),
    (
        'Lato w Parku',
        'Caly dzien atrakcji w upalne lato.',
        '2024-06-25 09:00:00',
        '2024-06-25 22:00:00',
        25.00
    ),
    (
        'Noc Filmowa',
        'Nocne pokazy filmowe na swiezym powietrzu.',
        '2024-07-05 21:00:00',
        '2024-07-06 03:00:00',
        18.00
    ),
    (
        'Koncert Letni',
        'Koncert muzyczny dla calej rodziny.',
        '2024-08-10 18:00:00',
        '2024-08-10 22:00:00',
        40.00
    ),
    (
        'Zimowe Swieto',
        'Zimowe atrakcje w swiatecznej atmosferze.',
        '2024-12-15 12:00:00',
        '2024-12-15 18:00:00',
        20.00
    ),
    (
        'Jesienne Noce',
        'Kolorowe jesienne atrakcje.',
        '2024-10-10 17:00:00',
        '2024-10-10 23:00:00',
        22.00
    ),
    (
        'Piknik Rodzinny',
        'Relaksacyjny dzien pelen gier i zabaw.',
        '2024-09-10 10:00:00',
        '2024-09-10 18:00:00',
        15.00
    ),
    (
        'Festiwal Kolorow',
        'Dzien pelen kolorowych atrakcji.',
        '2024-05-05 09:00:00',
        '2024-05-05 21:00:00',
        18.00
    );
GO

-- INSERT dla tabeli `Attractions`:

INSERT INTO
    Attractions (
        ID_Attraction_Categories,
        ID_Users,
        ID_Employees,
        ID_Events,
        Name,
        Description,
        Type,
        Capacity,
        Status,
        Opening_Date,
        Closing_Date,
        Maintenance_Date,
        Ticket_Price,
        Available_Slots,
        Occupied_Slots,
        Age_Restriction,
        Supervisor,
        Location
    )
VALUES
    (
        1,
        NULL,
        1,
        NULL,
        'Mega Kolejka',
        'Superszybka jazda dla lubiacych adrenaline!',
        'Ekstremalna',
        60,
        'Active',
        '2024-03-01',
        '2024-12-31',
        '2024-05-15',
        25.00,
        60,
        0,
        15,
        'Jan Kowalski',
        'Strefa Adrenaliny'
    ),
    (
        2,
        2,
        NULL,
        1,
        'Diabelski Mlyn',
        'Spokojna przejazdzka z pieknym widokiem.',
        'Rodzinne',
        45,
        'Active',
        '2024-04-05',
        '2024-12-31',
        '2024-06-10',
        18.50,
        40,
        5,
        6,
        NULL,
        'Strefa Rodzinna'
    ),
    (
        3,
        3,
        NULL,
        NULL,
        'Zjezdzalnia Wodna',
        'Zjezdzalnia z widokiem na caly park!',
        'Wodne',
        60,
        'Active',
        '2024-06-01',
        '2024-09-30',
        '2024-07-15',
        20.00,
        50,
        10,
        8,
        NULL,
        'Strefa Wodna'
    ),
    (
        4,
        NULL,
        4,
        NULL,
        'Karuzela dla Dzieci',
        'Mala karuzela dla najmlodszych.',
        'Dla dzieci',
        35,
        'Active',
        '2024-05-15',
        '2024-12-31',
        NULL,
        10.00,
        30,
        5,
        3,
        'Piotr Zielinski',
        'Strefa Dziecieca'
    ),
    (
        5,
        5,
        NULL,
        NULL,
        'Lesna Przygoda',
        'Spacer po lesie pelnym niespodzianek.',
        'Przygodowe',
        20,
        'Active',
        '2024-04-01',
        '2024-10-31',
        NULL,
        15.00,
        20,
        0,
        5,
        NULL,
        'Strefa Lesna'
    ),
    (
        6,
        6,
        NULL,
        NULL,
        'Strefa Relaksu',
        'Idealne miejsce na odpoczynek.',
        'Relaksacyjne',
        100,
        'Active',
        '2024-03-20',
        '2024-11-30',
        NULL,
        5.00,
        100,
        0,
        0,
        NULL,
        'Strefa Relaksu'
    ),
    (
        7,
        NULL,
        7,
        NULL,
        'Zimowe Wesole Miasteczko',
        'Sezonowa atrakcja z grami i zabawami.',
        'Sezonowe',
        80,
        'Active',
        '2024-12-01',
        '2025-02-28',
        NULL,
        12.00,
        80,
        0,
        0,
        'Ewa Kowalska',
        'Strefa Zimowa'
    ),
    (
        8,
        8,
        NULL,
        NULL,
        'Interaktywne Show',
        'Pokaz angazujacy publicznosc.',
        'Interaktywne',
        170,
        'Active',
        '2024-07-01',
        '2024-09-30',
        NULL,
        30.00,
        150,
        20,
        12,
        NULL,
        'Strefa Show'
    ),
    (
        1,
        9,
        NULL,
        NULL,
        'Ekstremalna Kolejka',
        'Najbardziej ekstremalna atrakcja w parku!',
        'Ekstremalna',
        55,
        'Active',
        '2024-06-15',
        '2024-12-31',
        '2024-08-01',
        35.00,
        50,
        5,
        16,
        NULL,
        'Strefa Ekstremalna'
    ),
    (
        3,
        NULL,
        10,
        NULL,
        'Basen Fale',
        'Basen z generowanymi falami.',
        'Wodne',
        80,
        'Active',
        '2024-06-01',
        '2024-09-30',
        NULL,
        25.00,
        70,
        10,
        6,
        'Adam Wojciechowski',
        'Strefa Basenowa'
    );
GO

-- INSERT dla tabeli `Stores`:

INSERT INTO
    Stores (
        ID_Employees,
        Name,
        Location,
        Opening_Hours,
        Contact_Information,
        Supervisor,
        Rating
    )
VALUES
    (
        1,
        'Sklep Pamiatkowy',
        'Park Rozrywki - Centrum',
        '9:00 - 20:00',
        'Tel: 123-456-789',
        NULL,
        4.9
    ),
    (
        2,
        'Restauracja Rodzinna',
        'Park Rozrywki - Wschod',
        '10:00 - 22:00',
        'Tel: 987-654-321',
        'Anna Nowak',
        4.3
    ),
    (
        3,
        'Kawiarnia przy Wejsciu',
        'Park Rozrywki - Wejscie Glowne',
        '8:00 - 18:00',
        'Tel: 234-567-890',
        'Piotr Zielinski',
        NULL
    ),
    (
        4,
        'Sklep z Gadzetami',
        'Park Rozrywki - Zachod',
        '9:00 - 21:00',
        'Tel: 345-678-901',
        NULL,
        NULL
    ),
    (
        5,
        'Bar Letni',
        'Park Rozrywki - Plaza',
        '10:00 - 20:00',
        'Tel: 456-789-012',
        'Tomasz Wojcik',
        NULL
    ),
    (
        6,
        'Lodziarnia',
        'Park Rozrywki - Centrum',
        '10:00 - 20:00',
        'Tel: 567-890-123',
        'Katarzyna Jankowska',
        NULL
    ),
    (
        7,
        'Kiosk z Pamiatkami',
        'Park Rozrywki - Wejscie Zachodnie',
        '9:00 - 18:00',
        'Tel: 678-901-234',
        NULL,
        NULL
    ),
    (
        8,
        'Bufet przy Scenie',
        'Park Rozrywki - Scena Glowna',
        '11:00 - 23:00',
        'Tel: 789-012-345',
        'Marek Mazur',
        NULL
    ),
    (
        9,
        'Strefa Grillowa',
        'Park Rozrywki - Plaza',
        '12:00 - 22:00',
        'Tel: 890-123-456',
        'Ewa Kowalska',
        NULL
    ),
    (
        10,
        'Herbaciarnia',
        'Park Rozrywki - Strefa Relaksu',
        '10:00 - 19:00',
        'Tel: 901-234-567',
        NULL,
        NULL
    );
GO

-- INSERT dla tabeli `Coupons`:

INSERT INTO
    Coupons (
        ID_Users,
        ID_Stores,
        Code,
        Expiry_Date,
        Discount_Percentage,
        Discount_Amount,
        Single_Use,
        Multiple_Uses,
        Applicable_Attractions,
        Applicable_Events
    )
VALUES
    (
        1,
        1,
        'KOD12345',
        '2024-11-30',
        10.00,
        NULL,
        'No',
        5,
        'Wszystkie',
        NULL
    ),
    (
        2,
        2,
        'ZNI12345',
        '2024-12-15',
        NULL,
        15.00,
        'Yes',
        1,
        NULL,
        'Halloween Event'
    ),
    (
        3,
        1,
        'SUMMER2024',
        '2024-08-31',
        20.00,
        NULL,
        'No',
        10,
        'Kolejka Kosmiczna',
        NULL
    ),
    (
        4,
        2,
        'SPRING23',
        '2024-04-30',
        10.00,
        NULL,
        'Yes',
        1,
        NULL,
        'Dzien Rodziny'
    ),
    (
        5,
        1,
        'HALLOWEEN20',
        '2024-10-31',
        NULL,
        20.00,
        'Yes',
        1,
        'Dom Strachu',
        'Halloween Event'
    ),
    (
        6,
        2,
        'DISCOUNT10',
        '2024-12-31',
        10.00,
        NULL,
        'No',
        5,
        'Wszystkie',
        NULL
    ),
    (
        7,
        3,
        'WINTERSALE',
        '2024-12-15',
        15.00,
        NULL,
        'Yes',
        1,
        NULL,
        'Swiateczny Event'
    ),
    (
        8,
        1,
        'NEWYEAR',
        '2025-01-10',
        NULL,
        25.00,
        'No',
        8,
        'Gorska Kolejka',
        NULL
    ),
    (
        9,
        2,
        'KOD2024',
        '2024-12-31',
        10.00,
        NULL,
        'No',
        3,
        'Diabelski Mlyn',
        NULL
    ),
    (
        10,
        3,
        'SUMMERFUN',
        '2024-09-01',
        20.00,
        NULL,
        'Yes',
        1,
        'Mega Kolejka',
        'Summer Event'
    );
GO

-- INSERT dla tabeli `Orders`:

INSERT INTO
    Orders (ID_Users, Order_Date, Total_Price, Status)
VALUES
    (1, '2024-05-25 14:20:10', 50.00, 'Confirmed'),
    (2, '2024-06-01 12:45:00', 20.00, 'Shipped'),
    (3, '2024-06-15 13:10:00', 40.00, 'Pending'),
    (4, '2024-07-10 16:30:00', 25.00, 'Cancelled'),
    (5, '2024-07-20 10:00:00', 25.98, 'Delivered'),
    (6, '2024-08-01 14:20:00', 18.00, 'Confirmed'),
    (7, '2024-08-15 11:15:00', 20.00, 'Shipped'),
    (8, '2024-09-05 15:30:00', 25.99, 'Confirmed'),
    (9, '2024-09-15 09:45:00', 37.00, 'Delivered'),
    (10, '2024-10-01 13:55:00', 40.00, 'Cancelled');
GO

-- INSERT dla tabeli `Products`:

INSERT INTO
    Products (
        Name,
        Description,
        Price,
        Stock_Quantity,
        Status,
        Rating
    )
VALUES
    (
        'Kubek Kolejkowicza',
        'Kubek dla fanow kolejek gorskich.',
        12.99,
        20,
        'Active',
        4.7
    ),
    (
        'Poduszka Parkowa',
        'Poduszka z logo parku rozrywki.',
        20.00,
        25,
        'Active',
        4.5
    ),
    (
        'Ksiazka Parkowa',
        'Ilustrowana historia parku.',
        25.99,
        35,
        'Active',
        4.9
    ),
    (
        'T-shirt Parkowy',
        'Koszulka z logo parku.',
        15.50,
        33,
        'Active',
        4.6
    ),
    (
        'Zestaw Naklejek',
        'Naklejki z atrakcjami parku.',
        5.00,
        110,
        'Active',
        4.4
    ),
    (
        'Plecak Parkowy',
        'Lekki plecak z logo parku.',
        30.00,
        55,
        'Active',
        4.8
    ),
    (
        'Smycz na klucze',
        'Smycz z atrakcjami parku.',
        3.99,
        100,
        'Active',
        4.2
    ),
    (
        'Kapelusz Letni',
        'Kapelusz z logo parku.',
        12.50,
        20,
        'Active',
        4.3
    ),
    (
        'Butelka Termiczna',
        'Butelka z logo parku, utrzymujaca temperature.',
        18.99,
        12,
        'Active',
        4.7
    ),
    (
        'Koc Piknikowy',
        'Koc z logo parku rozrywki.',
        24.99,
        5,
        'Active',
        4.5
    );
GO

-- INSERT dla tabeli `Order_Items`:

INSERT INTO
    Order_Items (
        ID_Orders,
        ID_Attractions,
        ID_Events,
        ID_Products,
        Quantity,
        Unit_Price
    )
VALUES
    (1, 1, NULL, NULL, 2, 25.00),
    (2, NULL, 2, NULL, 1, 20.00),
    (3, 3, NULL, NULL, 2, 20.00),
    (4, NULL, 4, NULL, 1, 25.00),
    (5, NULL, NULL, 1, 2, 12.99),
    (6, NULL, 5, NULL, 1, 18.00),
    (7, NULL, NULL, 2, 1, 20.00),
    (8, NULL, NULL, 3, 1, 25.99),
    (9, 2, NULL, NULL, 2, 18.50),
    (10, 4, NULL, NULL, 4, 10.00);
GO

-- INSERT dla tabeli `Promotions`:

INSERT INTO
    Promotions (
        ID_Attractions,
        ID_Events,
        ID_Products,
        Name,
        Description,
        Start_Date,
        End_Date,
        Discount_Percentage,
        Discount_Amount,
        Applicable_Attractions,
        Applicable_Events,
        Status
    )
VALUES
    (
        1,
        NULL,
        NULL,
        'Sezonowa Znizka!',
        'Znizka 10% na wszystkie bilety!',
        '2024-11-01 09:00:00',
        '2024-11-30 22:00:00',
        10.00,
        NULL,
        'Wszystkie',
        NULL,
        'Active'
    ),
    (
        NULL,
        1,
        NULL,
        'Promocja na Halloween',
        'Specjalna znizka 20% na Halloween!',
        '2024-10-01 09:00:00',
        '2024-10-31 23:00:00',
        20.00,
        NULL,
        NULL,
        'Halloween Event',
        'Active'
    ),
    (
        NULL,
        NULL,
        1,
        'Znizka na Kubek',
        'Kubek w promocyjnej cenie!',
        '2024-07-01 08:00:00',
        '2024-07-31 23:59:00',
        NULL,
        2.00,
        NULL,
        NULL,
        'Active'
    ),
    (
        2,
        NULL,
        NULL,
        'Znizka Letnia',
        'Znizka 15% na wybrane atrakcje letnie.',
        '2024-06-01 09:00:00',
        '2024-08-31 20:00:00',
        15.00,
        NULL,
        'Diabelski Mlyn',
        NULL,
        'Active'
    ),
    (
        3,
        NULL,
        NULL,
        'Rodzinna Promocja',
        'Bilety rodzinne z 5$ rabatem.',
        '2024-05-10 10:00:00',
        '2024-12-31 23:59:00',
        NULL,
        5.00,
        'Wszystkie',
        NULL,
        'Active'
    ),
    (
        NULL,
        NULL,
        2,
        'Znizka na Poduszki',
        'Poduszki w nizszej cenie.',
        '2024-06-01 08:00:00',
        '2024-06-30 23:59:00',
        10.00,
        NULL,
        NULL,
        NULL,
        'Active'
    ),
    (
        4,
        5,
        NULL,
        'Promocja Wakacyjna',
        'Atrakcje i festiwale wakacyjne z rabatem.',
        '2024-07-01 09:00:00',
        '2024-08-15 23:00:00',
        NULL,
        20.00,
        'Wszystkie',
        'Festiwal Kolorow',
        'Active'
    ),
    (
        NULL,
        8,
        NULL,
        'Znizka Jesienna',
        'Znizka 15% na jesienne atrakcje.',
        '2024-10-01 09:00:00',
        '2024-10-31 20:00:00',
        15.00,
        NULL,
        NULL,
        'Jesienne Noce',
        'Active'
    ),
    (
        5,
        NULL,
        NULL,
        'Dzien Matki',
        'Specjalna promocja z okazji Dnia Matki!',
        '2024-05-25 09:00:00',
        '2024-05-25 22:00:00',
        25.00,
        NULL,
        'Mlyn Wodny',
        NULL,
        'Inactive'
    ),
    (
        6,
        2,
        3,
        'Swieto Wiosny',
        'Swiateczna promocja w dniu festiwalu!',
        '2024-03-20 09:00:00',
        '2024-03-20 20:00:00',
        NULL,
        5.00,
        'Kolejka Kosmiczna',
        'Festiwal Wiosenny',
        'Active'
    );
GO

-- INSERT dla tabeli `Reservations`:

INSERT INTO
    Reservations (
        ID_Users,
        ID_Attractions,
        ID_Events,
        Reservation_Date,
        Status,
        Total_Cost,
        Number_Of_People,
        Special_Request,
        Reservation_Code
    )
VALUES
    (
        1,
        1,
        NULL,
        '2024-08-05',
        'Confirmed',
        75.00,
        3,
        'Preferowane miejsce przy oknie.',
        'R567890'
    ),
    (
        2,
        NULL,
        1,
        '2024-09-20',
        'Pending',
        120.00,
        4,
        'Brak specjalnych uwag',
        'R345678'
    ),
    (
        3,
        3,
        NULL,
        '2024-10-10',
        'Confirmed',
        100.00,
        5,
        'Obok wejscia',
        'R890123'
    ),
    (
        4,
        4,
        NULL,
        '2024-11-01',
        'Cancelled',
        20.00,
        2,
        'Blisko wyjscia',
        'R456789'
    ),
    (
        5,
        NULL,
        2,
        '2024-11-15',
        'Confirmed',
        40.00,
        3,
        'Narozne miejsce',
        'R123456'
    ),
    (
        6,
        5,
        NULL,
        '2024-07-25',
        'Pending',
        60.00,
        4,
        'Bez specjalnych wymagan',
        'R654321'
    ),
    (
        7,
        NULL,
        3,
        '2024-06-10',
        'Confirmed',
        15.00,
        1,
        'Brak',
        'R987654'
    ),
    (
        8,
        NULL,
        4,
        '2024-08-01',
        'Confirmed',
        125.00,
        5,
        'Centralne miejsce',
        'R789012'
    ),
    (
        9,
        7,
        NULL,
        '2024-08-15',
        'Cancelled',
        24.00,
        2,
        'W cieniu',
        'R678901'
    ),
    (
        10,
        2,
        NULL,
        '2024-10-31',
        'Confirmed',
        74.00,
        4,
        'Blisko sceny',
        'R234567'
    );
GO

-- INSERT dla tabeli `Reviews`:

INSERT INTO
    Reviews (
        ID_Users,
        ID_Employees,
        ID_Attractions,
        ID_Products,
        ID_Stores,
        Rating,
        Comment,
        Date,
        User_Rating,
        Staff_Rating,
        Cleanliness_Rating
    )
VALUES
    (
        2,
        1,
        1,
        NULL,
        NULL,
        5.0,
        'Swietna obsluga i niesamowita atrakcja!',
        '2024-07-15 13:45:23',
        NULL,
        5.0,
        4.9
    ),
    (
        1,
        2,
        NULL,
        2,
        1,
        4.3,
        'Bardzo mila obsluga i dobry asortyment.',
        '2024-08-10 16:12:05',
        3.7,
        4.5,
        4.6
    ),
    (
        3,
        3,
        2,
        NULL,
        NULL,
        4.8,
        'Widok z mlyna byl niesamowity!',
        '2024-06-01 10:20:00',
        NULL,
        4.7,
        4.8
    ),
    (
        4,
        4,
        NULL,
        3,
        NULL,
        4.6,
        'Ksiazka bardzo ciekawa, polecam.',
        '2024-06-05 12:30:00',
        NULL,
        4.6,
        NULL
    ),
    (
        5,
        NULL,
        NULL,
        NULL,
        2,
        4.1,
        'Szeroki wybor gadzetow, polecam!',
        '2024-05-18 14:50:00',
        NULL,
        NULL,
        4.1
    ),
    (
        6,
        5,
        NULL,
        NULL,
        1,
        4.8,
        'Rewelacyjna obsluga i sklep, szybko i milo!',
        '2024-04-22 10:10:00',
        4.5,
        5.0,
        5.0
    ),
    (
        7,
        6,
        5,
        NULL,
        NULL,
        4.7,
        'Relaksujaca przejazdzka nad woda.',
        '2024-05-03 15:00:00',
        NULL,
        4.8,
        4.7
    ),
    (
        8,
        NULL,
        NULL,
        NULL,
        1,
        4.9,
        'Sympatyczny sklep.',
        '2024-07-02 11:20:00',
        NULL,
        NULL,
        4.9
    ),
    (
        9,
        7,
        NULL,
        NULL,
        2,
        3.9,
        'Produkty swietnej jakosci.',
        '2024-08-14 14:30:00',
        3.0,
        4.3,
        4.4
    ),
    (
        10,
        8,
        3,
        NULL,
        NULL,
        4.9,
        'Doskonała atrakcja dla dzieci!',
        '2024-06-28 12:00:00',
        NULL,
        4.9,
        4.9
    );
GO

-- INSERT dla tabeli `Store_Inventory`:

INSERT INTO
    Store_Inventory (ID_Stores, ID_Products, Quantity_In_Stock)
VALUES
    (1, 1, 20),
    (2, 2, 10),
    (3, 3, 25),
    (4, 4, 15),
    (5, 5, 50),
    (6, 6, 30),
    (7, 7, 100),
    (8, 8, 20),
    (9, 9, 12),
    (10, 10, 5),
    (1, 2, 15),
    (2, 3, 10),
    (3, 4, 18),
    (4, 5, 60),
    (5, 6, 25);
GO

-- INSERT dla tabeli `Tickets`:

INSERT INTO
    Tickets (
        ID_Users,
        ID_Attractions,
        ID_Events,
        Ticket_Number,
        Purchase_Date,
        Expiry_Date,
        Status,
        Price,
        Ticket_Type
    )
VALUES
    (
        1,
        1,
        NULL,
        'TIC1234567',
        '2024-07-10',
        '2024-07-17',
        'Used',
        25.00,
        'Jednorazowy'
    ),
    (
        2,
        NULL,
        1,
        'TIC2345678',
        '2024-08-05',
        '2024-08-12',
        'Active',
        30.00,
        'Jednorazowy'
    ),
    (
        3,
        NULL,
        2,
        'TIC3456789',
        '2024-09-01',
        '2024-09-30',
        'Expired',
        40.00,
        'Rodzinny'
    ),
    (
        4,
        3,
        NULL,
        'TIC4567890',
        '2024-06-10',
        '2024-06-17',
        'Cancelled',
        20.00,
        'Jednorazowy'
    ),
    (
        5,
        2,
        NULL,
        'TIC5678901',
        '2024-07-25',
        '2024-08-25',
        'Active',
        108.50,
        'Miesieczny'
    ),
    (
        6,
        5,
        NULL,
        'TIC6789012',
        '2024-05-15',
        '2024-09-15',
        'Used',
        1050.00,
        'Sezonowy'
    ),
    (
        7,
        NULL,
        5,
        'TIC7890123',
        '2024-09-05',
        '2024-09-12',
        'Active',
        18.00,
        'Jednorazowy'
    ),
    (
        8,
        6,
        NULL,
        'TIC8901234',
        '2024-10-01',
        '2024-10-31',
        'Cancelled',
        10.00,
        'Rodzinny'
    ),
    (
        9,
        NULL,
        6,
        'TIC9012345',
        '2024-10-15',
        '2024-10-22',
        'Active',
        40.00,
        'Jednorazowy'
    ),
    (
        10,
        7,
        NULL,
        'TIC0123456',
        '2024-11-01',
        '2024-12-01',
        'Used',
        120.00,
        'Miesieczny'
    );
GO