
JWT 
````
dotnet add package Microsoft.AspNetCore.Authentication
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6
dotnet add package Microsoft.IdentityModel.Tokens 
dotnet add package System.IdentityModel.Tokens.Jwt 

````
Para rodar o Dockerfile
docker buikd -t apitools
docker run --name apitools -d -t 8000:80 apitools


Migration
*package
Microsoft.EntityFrameworkCore 
Microsoft.EntityFrameworkCore.Tools 

````
dotnet tool install --global dotnet-ef
dotnet ef --version
dotnet ef migrations add Produtos
dotnet ef migrations add InitialCreate
dotnet ef database update
````





### Query
CREATE TABLE User (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Role VARCHAR(255) DEFAULT NULL
);
CREATE TABLE Produtos (
    id INT NOT NULL AUTO_INCREMENT,
    image VARCHAR(255),
    name VARCHAR(100),
    description TEXT,
    price DECIMAL(10, 2),
    quantity INT DEFAULT 0,
    amount DECIMAL(10, 2) DEFAULT 0,
    PRIMARY KEY (id),
    id_variantions,
);
ALTER TABLE Produtos ADD COLUMN variations JSON;


CREATE TABLE ProdutoVariations (
    id INT NOT NULL AUTO_INCREMENT,
    product_id INT,
    name VARCHAR(50),
    PRIMARY KEY (id),
    FOREIGN KEY (product_id) REFERENCES Produtos(id) ON DELETE CASCADE
);

select * from ProdutoVariations p 

CREATE TABLE Produtos (
    id INT NOT NULL AUTO_INCREMENT,
    image VARCHAR(255),
    name VARCHAR(100),
    description TEXT,
    price DECIMAL(10, 2),
    quantity INT DEFAULT 0,
    amount DECIMAL(10, 2) DEFAULT 0,
    id_variantions INT, -- coluna para chave estrangeira
    PRIMARY KEY (id),
    FOREIGN KEY (id_variantions) REFERENCES ProdutoVariations(id) -- definindo a chave estrangeira
);

INSERT INTO Produtos (id, image, name, description, price, quantity, amount) VALUES
(1, 'http://localhost:4200/assets/images/coffees/Coffee-1.png', 'Expresso Tradicional', 'O Tradicional café feito com água quente e grãos moídos', 2.90, 0, 0),
(2, 'http://localhost:4200/assets/images/coffees/Coffee-2.png', 'Expresso Americano', 'Expresso diluido, menos intenso que o tradicional', 3.20, 0, 0),
(3, 'http://localhost:4200/assets/images/coffees/Coffee-3.png', 'Expresso Cremoso', 'Café expresso tradicional com espuma', 3.50, 0, 0),
(4, 'http://localhost:4200/assets/images/coffees/Coffee-4.png', 'Expresso Gelado', 'Bebida preparada com café expresso e cubos de gelo', 3.50, 0, 0),
(5, 'http://localhost:4200/assets/images/coffees/Coffee-5.png', 'Café com leite', 'Meio a meio de expresso tradicional com leite vaporizado', 3.90, 0, 0),
(6, 'http://localhost:4200/assets/images/coffees/Coffee-6.png', 'Latte', 'Uma dose de café expresso com o dobro de leite e espuma cremosa', 4.00, 0, 0),
(7, 'http://localhost:4200/assets/images/coffees/Coffee-7.png', 'Capuccino', 'Bebida com canela feita de doses iguais de café, leite e espuma', 4.00, 0, 0),
(8, 'http://localhost:4200/assets/images/coffees/Coffee-8.png', 'Macchiato', 'Café expresso misturado com um pouco de leite quente e espuma', 4.00, 0, 0),
(9, 'http://localhost:4200/assets/images/coffees/Coffee-9.png', 'Mocaccino', 'Café expresso com calda de chocolate, pouco leite e espuma', 4.20, 0, 0),
(10, 'http://localhost:4200/assets/images/coffees/Coffee-10.png', 'Chocolate Quente', 'Bebida feita com chocolate dissolvido no leite quente e café', 5.00, 0, 0),
(11, 'http://localhost:4200/assets/images/coffees/Coffee-11.png', 'Cubano', 'Drink gelado de café expresso com rum, creme de leite e hortelã', 6.00, 0, 0),
(12, 'http://localhost:4200/assets/images/coffees/Coffee-12.png', 'Havaiano', 'Bebida adocicada preparada com café e leite de coco', 6.00, 0, 0),
(13, 'http://localhost:4200/assets/images/coffees/Coffee-13.png', 'Árabe', 'Bebida preparada com grãos de café árabe e especiarias', 7.00, 0, 0),
(14, 'http://localhost:4200/assets/images/coffees/Coffee-14.png', 'Irlandês', 'Bebida a base de café, uísque irlandês, açúcar e chantilly', 8.00, 0, 0);

INSERT INTO ProdutoVariations (product_id, name) VALUES
(1, 'tradicional'),
(2, 'tradicional'),
(3, 'tradicional'),
(4, 'tradicional'),
(4, 'Gelado'),
(5, 'tradicional'),
(5, 'Com leite'),
(6, 'tradicional'),
(6, 'com leite'),
(7, 'tradicional'),
(7, 'com leite'),
(8, 'tradicional'),
(9, 'tradicional'),
(9, 'com leite'),
(10, 'tradicional'),
(10, 'com leite'),
(11, 'tradicional'),
(11, 'alcoólico'),
(11, 'Gelado'),
(12, 'especial'),
(13, 'especial'),
(14, 'especial'),
(14, 'alcoólico');


select * from ProdutoVariations
select * from Produtos p2 


select p.*, v.name  from Produtos p, ProdutoVariations v where v.product_id = p.id  order by 1, v.name





