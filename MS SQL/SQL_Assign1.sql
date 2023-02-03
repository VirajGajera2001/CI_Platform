create TABLE Products(
	ProductID int not null,	ProductName nvarchar(50) not null,	SupplierID int not null,	CategoryID int not null,	QuantityPerUnit int not null,	UnitPrice int not null,	UnitsInStock int not null,	UnitsOnOrder int not null,	ReorderLevel int not null,	Discontinued nvarchar(10) not null
);


select * from Products;


insert into Products (ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued)
values (100,'realme3',2001,1,10,10,50,2,3,'no');


/*Query1*/

select ProductID,ProductName,UnitPrice from Products where UnitPrice<20; 


/*Query2*/

select ProductID,ProductName,UnitPrice from Products where UnitPrice between 15 and 25;


/*Query3*/

select ProductName,UnitPrice from Products where Unitprice>(select AVG(UnitPrice) from Products);


/*Query4*/

select top 10 ProductName,UnitPrice from Products order by UnitPrice desc;


/*Query5*/

select Discontinued, count(CategoryID) as no_of_products
from Products
group by Discontinued


/*Query6*/

select ProductName,UnitsOnOrder,UnitsInStock
from Products
where UnitsInStock < UnitsOnOrder



