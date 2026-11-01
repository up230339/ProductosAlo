CREATE DATABASE ProductosDB;
GO

USE ProductosDB;
GO

CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(250) NULL,
    Precio DECIMAL(10,2) NOT NULL CHECK (Precio >= 0)
);
GO

INSERT INTO Productos (Nombre, Descripcion, Precio) VALUES
('Cuaderno', 'Cuaderno profesional de 100 hojas', 45.50),
('Pluma', 'Pluma de tinta azul', 12.00),
('Mochila', 'Mochila escolar', 350.00);
GO
