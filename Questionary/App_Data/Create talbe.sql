﻿CREATE TABLE Clients
(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Name NVARCHAR(MAX) NOT NULL,
LastName NVARCHAR(MAX) NOT NULL,
Age INT NOT NULL
)
CREATE TABLE Test
(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DateComplete DATETIME NOT NULL,
CapitalKZ NVARCHAR(MAX) NOT NULL,
CountCityKZ INT NOT NULL,
LanguageKZ NVARCHAR(25) NOT NULL,
DateFounded DATETIME NOT NULL,
ClientsId INT FOREIGN KEY REFERENCES Clients(Id)
)