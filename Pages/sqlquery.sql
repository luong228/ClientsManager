CREATE TABLE clients (
	id INT NOT NULL PRIMARY KEY IDENTITY,
	name VARCHAR (100) NOT NULL,
	email VARCHAR (100) NOT NULL UNIQUE,
	phone VARCHAR (20) NULL,
	address VARCHAR (100) NULL,
	create_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO clients (name, email, phone, address)
VALUES
('Bill Gates', 'bill.gates@microsoft.com', '+123456789', 'New York, USA'),
('Elon Musk', 'elon.musk@space.com', '+123456789', 'New York, USA'),
('Cristiano Ronaldo', 'ronaldo@gmail.com', '+123456789', 'New York, USA')
