CREATE SCHEMA Test;

SELECT name, schema_id, principal_id 
FROM sys.schemas;

-- 1. Criar a tabela de Empresas (Company)
CREATE TABLE Test.Companies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Country NVARCHAR(100) NOT NULL
);

-- 2. Criar a tabela de Funcionários (Employee)
CREATE TABLE Test.Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Position NVARCHAR(100) NOT NULL,
    CompanyId INT NOT NULL,
    
    -- Cria o relacionamento entre as tabelas
    CONSTRAINT FK_Employee_Company FOREIGN KEY (CompanyId) 
    REFERENCES Test.Companies(Id)
);


----
CREATE PROCEDURE CompaniesLista_sps
AS
BEGIN
    SELECT 
        c.id,
        c.Name,
        c.Address,
        c.Country
    FROM 
        Test.Companies c;
END;
GO

----
CREATE PROCEDURE CompaniesObter_sps
    @Id INT
AS
BEGIN
    SELECT 
        c.id,
        c.Name,
        c.Address,
        c.Country
    FROM 
        Test.Companies c
    WHERE 
        Id = @Id;
END;
GO

-- Buscar por ID e Nome (Filtro dinâmico)
CREATE PROCEDURE CompaniesObterFiltroDinamico_sps
    @Id INT,
    @Name NVARCHAR(100) = NULL
AS
BEGIN
    SELECT 
        c.id,
        c.Name,
        c.Address,
        c.Country
    FROM 
        Test.Companies c
    WHERE
        c.Id = @Id AND 
        (@Name IS NULL OR c.Name = @Name);
END;
GO

-- Criar Empresa
CREATE PROCEDURE Companies_spi
    @Name NVARCHAR(100),
    @Address NVARCHAR(255),
    @Country NVARCHAR(100)
AS
BEGIN
    INSERT INTO Test.Companies 
    (
        Name, 
        Address, 
        Country
    )
    VALUES 
    (
        @Name, 
        @Address, 
        @Country
    );
    
    SELECT CAST(SCOPE_IDENTITY() as int);
END;
GO

-- Atualizar Empresa
CREATE PROCEDURE Companies_spu
    @Id INT,
    @Name NVARCHAR(100),
    @Address NVARCHAR(255),
    @Country NVARCHAR(100)
AS
BEGIN
    UPDATE Test.Companies 
    SET 
        Name = @Name,
        Address = @Address, 
        Country = @Country 
    WHERE Id = @Id;
END;
GO

ALTER SCHEMA Test TRANSFER dbo.Companies_spi;
ALTER SCHEMA Test TRANSFER dbo.Companies_spu;
ALTER SCHEMA Test TRANSFER dbo.CompaniesLista_sps;
ALTER SCHEMA Test TRANSFER dbo.CompaniesObter_sps;
ALTER SCHEMA Test TRANSFER dbo.CompaniesObterFiltroDinamico_sps;




SELECT * FROM Test.Companies;