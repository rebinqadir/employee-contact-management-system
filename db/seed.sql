-- DATA POPULATION
-- =======================================================
-- Sample Companies
INSERT INTO dbo.Companies (CompanyName, Domain, Industry, Website)
VALUES
('Alpha Corp', 'alpha.com', 'Technology', 'https://alpha.com'),
('Beta Ltd', 'beta.org', 'Finance', 'https://beta.org'),
('Gamma Inc', 'gamma.net', 'Healthcare', 'https://gamma.net'),
('Delta LLC', 'delta.io', 'Retail', 'https://delta.io'),
('Epsilon GmbH', 'epsilon.de', 'Manufacturing', 'https://epsilon.de');
GO

-- Sample Employees
INSERT INTO dbo.Employees (Name, Email, Phone, JobTitle, CompanyID)
VALUES
('Alice Smith', 'alice@alpha.com', '555-0100', 'Software Engineer', 1),
('Bob Jones', 'bob@alpha.com', '555-0101', 'QA Analyst', 1),
('Carol White', 'carol@beta.org', '555-0102', 'Accountant', 2),
('David Brown', 'david@beta.org', '555-0103', 'Financial Analyst', 2),
('Eve Davis', 'eve@gamma.net', '555-0104', 'Nurse', 3),
('Frank Moore', 'frank@gamma.net', '555-0105', 'Doctor', 3),
('Grace Wilson', 'grace@delta.io', '555-0106', 'Sales Manager', 4),
('Henry Taylor', 'henry@delta.io', '555-0107', 'Store Manager', 4),
('Ivy Anderson', 'ivy@epsilon.de', '555-0108', 'Production Supervisor', 5),
('Jack Thomas', 'jack@epsilon.de', '555-0109', 'Engineer', 5),
('Kara Martin', 'kara@alpha.com', '555-0110', 'HR Specialist', 1),
('Leo Harris', 'leo@beta.org', '555-0111', 'Tax Consultant', 2),
('Mona Clark', 'mona@gamma.net', '555-0112', 'Lab Technician', 3),
('Nate Lewis', 'nate@delta.io', '555-0113', 'Customer Service', 4),
('Olivia Walker', 'olivia@epsilon.de', '555-0114', 'Quality Inspector', 5);
GO