﻿CREATE TABLE Client
(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Nickname NVARCHAR(MAX) NOT NULL,
Email NVARCHAR(MAX) NOT NULL,
Password NVARCHAR(18) NOT NULL,

)