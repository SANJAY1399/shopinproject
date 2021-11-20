create table Users
(
userID INT PRIMARY KEY ,
UserName NVARCHAR(50),
UserPassword NVARCHAR(50),
Name Nvarchar(50),
Gender varchar(10),
emailid nvarchar(50),
Location nvarchar(50),
ph_number nvarchar(50)
);

Create table Category
(
CategoryID int primary key,
CategoryName nvarchar(30),
Image Varchar(50),
);

Create table Material(
MaterialID int primary key,
MaterialName nvarchar(20),
Image Varchar(50));

Create table cloth(
ClothID int primary key,
CategoryID int ,
MaterialID int,
clothName nvarchar(20),
Price nvarchar(10),
Image Varchar(50));

Alter table cloth add foreign key(CategoryID) references Category(CategoryID);
Alter table cloth add foreign key(MaterialID) references Material(MaterialID);

insert into Category values(1,'Kids','k.jpeg')
insert into Category values(2,'Men','m.jpeg')
insert into Category values(3,'Women','w.jpeg')

insert into Material values(1,'Cotton','cotton.jpg')
insert into Material values(2,'Denim','denim.jpg')
insert into Material values(3,'Silk','silk.jpg')
insert into Material values(4,'Wollen','woolen.jpg')

create view womenprods as
select cloth.ClothID,cloth.CategoryID, cloth.MaterialID,cloth.clothName,cloth.Price,cloth.Image
from cloth
where CategoryID=3;
create view menprods as
select cloth.ClothID,cloth.CategoryID, cloth.MaterialID,cloth.clothName,cloth.Price,cloth.Image
from cloth
where CategoryID=2;
create view kidprods as
select cloth.ClothID,cloth.CategoryID, cloth.MaterialID,cloth.clothName,cloth.Price,cloth.Image
from cloth
where CategoryID=1;

create table accdet
(
ID INT identity(1,1) PRIMARY KEY ,
Name NVARCHAR(50),
Address NVARCHAR(50),
PhoneNumber Nvarchar(50),
AlternatePhoneNumber varchar(10),
PinCode nvarchar(50),
City nvarchar(50),
);

create table usercart(
id int primary key identity(1,1),
username nvarchar(20),
ClothID int not null,
Image nvarchar(20),
clothName nvarchar(20),
Price nvarchar(10));

create table cart(
id int primary key identity(1,1),
ClothID int not null,
Image nvarchar(20),
clothName nvarchar(20),
Price nvarchar(10));

create table Userrole
(id int primary key identity(1,1),
UserID int not null,
Role nvarchar(20));

Alter table Userrole add foreign key(UserID) references Users(userID);