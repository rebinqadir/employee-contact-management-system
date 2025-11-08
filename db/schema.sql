-- =======================================================
-- 1️. DATABASE SETUP
-- =======================================================

-- Create the database
CREATE DATABASE EmployeeContactDb;
GO

-- Switch to the new database context
USE EmployeeContactDb;
GO


-- =======================================================
-- 2️. TABLE CREATION
-- =======================================================

-- Companies Table
CREATE TABLE dbo.Companies (
    -- Primary Key: Automatically creates a CLUSTERED index for fast row retrieval
    ID INT IDENTITY(1,1) PRIMARY KEY,                   
    -- UNIQUE constraint: Automatically creates a NONCLUSTERED index
    CompanyName NVARCHAR(255) NOT NULL UNIQUE,          
    -- UNIQUE constraint: Automatically creates a NONCLUSTERED index
    Domain NVARCHAR(255) NOT NULL UNIQUE,               
    Industry NVARCHAR(255) NULL,                        
    Website NVARCHAR(255) NULL                          
);
GO

-- Employees Table
CREATE TABLE dbo.Employees (
    -- Primary Key: Automatically creates a CLUSTERED index
    ID INT IDENTITY(1,1) PRIMARY KEY,                   
    Name NVARCHAR(255) NOT NULL,                        
    -- UNIQUE constraint: Automatically creates a NONCLUSTERED index
    Email NVARCHAR(255) NOT NULL UNIQUE,                
    Phone NVARCHAR(50) NULL,                            
    JobTitle NVARCHAR(255) NULL,                        
    CompanyID INT NOT NULL,                             
    -- Default value for status and high-precision timestamp
    IsActive BIT NOT NULL DEFAULT 1,                    
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(), 

    -- Constraint definitions
    CONSTRAINT CHK_CompanyID CHECK (CompanyID > 0),     
    CONSTRAINT FK_Employees_Companies FOREIGN KEY (CompanyID)
        REFERENCES dbo.Companies(Id)
        ON DELETE CASCADE                               
);
GO


-- =======================================================
-- 3️. INDEX OPTIMIZATION
-- =======================================================

-- Index I: Optimize JOINs on the Foreign Key (CompanyID)
CREATE NONCLUSTERED INDEX IX_Employees_CompanyID
ON dbo.Employees (CompanyID);
GO

-- Index II: Optimize searching and sorting by Employee Name
CREATE NONCLUSTERED INDEX IX_Employees_Name
ON dbo.Employees (Name);
GO

-- Index III: Optimize filtering for active/inactive employees
CREATE NONCLUSTERED INDEX IX_Employees_IsActive
ON dbo.Employees (IsActive);
GO