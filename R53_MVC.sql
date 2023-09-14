CREATE DATABASE R53_MVC
GO
USE R53_MVC
GO
CREATE TABLE Category
(
	CId INT PRIMARY KEY IDENTITY(1,1),
	CName VARCHAR(50) NOT NULL
)
GO
CREATE TABLE Book
(
	Id INT PRIMARY KEY IDENTITY,
	BookName VARCHAR(50) NOT NULL,
	Price MONEY NOT NULL,
	PublishDate DATETIME NOT NULL,
	PicturePath VARCHAR(MAX),
	Avialable BIT NOT NULL,
	CId INT NOT NULL,
	FOREIGN KEY (CId) REFERENCES Category(CId)
)
GO

INSERT INTO Category (CName)
VALUES
('Fiction'),
('Non-Fiction'),
('Mystery'),
('Science Fiction'),
('Romance'),
('Self-Help'),
('Biography'),
('Cooking'),
('Travel'),
('Science');


SELECT * From Category