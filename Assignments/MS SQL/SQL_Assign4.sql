/*Query1 */
create proc "calavgfreight of ordertbl"
@custid nvarchar(15)
as 
begin
	select O.CustomerID,AVG(O.Freight) as "average freight"
	from Orders O
	where O.CustomerID=@custid
	group by O.CustomerID
end

sp_helptext 'insertintoordertbl'

create proc insertintoordertbl2(@OrderID int, @CustomerID nchar(15), @EmployeeID int, @OrderDate datetime, @RequiredDate datetime, @ShippedDate datetime, @ShipVia int, @Freight money, @ShipName nvarchar(50), @ShipAddress nvarchar(60), @ShipCity nvarchar(20), @ShipRegion nvarchar(20), @ShipPostalCode nvarchar(20), @ShipCountry nvarchar(50))
as 
begin
	declare @avgfraight money
	set @avgfraight=(select AVG(Freight) from Orders where CustomerID=@CustomerID)


	if(@avgfraight>@Freight)
	begin
		insert into Orders(OrderID,CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry)
		values(@OrderID, @CustomerID, @EmployeeID, @OrderDate , @RequiredDate , @ShippedDate , @ShipVia , @Freight , @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry)
	end
	else
	begin
	raiserror ('inserted freight is more than customer avg freight',10,1)
	rollback transaction
	end
end

set identity_insert Orders on
exec insertintoordertbl2 '10246','VINET','5','','','','1','10','','','','','',''

create proc updateordertbl2(@OrderID int, @CustomerID nchar(15), @EmployeeID int, @OrderDate datetime, @RequiredDate datetime, @ShippedDate datetime, @ShipVia int, @Freight money, @ShipName nvarchar(50), @ShipAddress nvarchar(60), @ShipCity nvarchar(20), @ShipRegion nvarchar(20), @ShipPostalCode nvarchar(20), @ShipCountry nvarchar(50))
as
begin
	declare @avgfraight money
	set @avgfraight=(select AVG(Freight) from Orders where CustomerID=@CustomerID)

	if(@avgfraight>@Freight)
	begin
		update Orders
		set EmployeeID=@EmployeeID,
			OrderDate=@OrderDate,
			RequiredDate=@RequiredDate,
			ShippedDate=@ShippedDate,
			ShipVia=@ShipVia,
			Freight=@Freight,
			ShipName=@ShipName,
			ShipAddress=@ShipAddress,
			ShipCity=@ShipCity,
			ShipRegion=@ShipRegion,
			ShipPostalCode=@ShipPostalCode,
			ShipCountry=@ShipCountry
			where OrderID=@OrderID and CustomerID=@CustomerID
	end
	else
		begin
		raiserror('updated freight is more than avg freight',10,1)
		rollback transaction
	end
end

exec updateordertbl2 '10246','VINET','5','','','','2','10','','','','','',''

select * from Orders
where CustomerID='VINET'

/*Query2 */
create procedure "EmpSalesByCountry"
@Beginningdate DateTime,
@Endingdate DateTime
as
begin
select E.Country,E.LastName,E.FirstName,O.ShippedDate,O.OrderID,SUM(OD.Quantity*OD.UnitPrice) as "Price"
from Employees E
inner join Orders O
on O.EmployeeID=E.EmployeeID
inner join [Order Details] OD
on OD.OrderID=O.OrderID
where O.ShippedDate between @Beginningdate and @Endingdate
group by E.Country,E.FirstName,E.LastName,O.ShippedDate,O.OrderID
end

exec [EmpSalesByCountry] '1996/06/04','1997/06/11'

/*query3*/
create proc "SalesByYear"
@Beginningdate DateTime,
@Endingdate DateTime
as 
begin
select O.ShippedDate,O.OrderID,SUM(OD.Quantity*OD.UnitPrice) as "Total",YEAR(O.ShippedDate) as "Year"
from Orders O
inner join [Order Details] OD
on O.OrderID=OD.OrderID
where O.ShippedDate between @Beginningdate and @Endingdate
group by O.ShippedDate,O.OrderID
end

exec [SalesByYear] '1996/09/04','1997/06/11'

/*Query4 */
create proc "SaleByCategories"
@CategoryName nvarchar(15),
@ordyear nvarchar(4)
as
if @ordyear!='1996'or @ordyear!='1997' or @ordyear!='1998'
begin 
declare @msg nvarchar(50)
set @msg='please enter year among 1996,1997,1998'
print @msg
end 

select P.ProductName,SUM(P.UnitPrice*OD.Quantity) as "total purchase"
from Products P,[Order Details] OD,Orders O,Categories C
where O.OrderID=OD.OrderID
and P.ProductID=OD.ProductID
and C.CategoryID=P.CategoryID
and C.CategoryName=@CategoryName
group by P.ProductName
order by P.ProductName

exec [SaleByCategories] 'Seafood','2000'

/*query5 */
create proc "Tenmostexpensiveprocs"
as 
begin 
select top 10 P.ProductName as "Ten most expensive products",P.UnitPrice
from Products P
order by P.UnitPrice desc
end

exec [Tenmostexpensiveprocs]

/*Query6 */
create proc "insertintoordertbl"
@OrderID int,
@ProductID int,
@UnitPrice money,
@Quantity smallint,
@Discount real
as
begin
insert into [Order Details](OrderID,ProductID,UnitPrice,Quantity,Discount)
	values(@OrderID,@ProductID,@UnitPrice,@Quantity,@Discount)
end

exec [insertintoordertbl] '11077','1','25.65','46','0.5'

select * from [Order Details]
where OrderID=11077

/*Query7 */
create proc "updateorderdetailtbl"
@OrderID int,
@ProductID int,
@UnitPrice money,
@Quantity smallint,
@Discount real
as 
begin
		update [Order Details]
		set UnitPrice=@UnitPrice,Quantity=@Quantity,Discount=@Discount
		where OrderID=@OrderID and ProductID=@ProductID
end

exec [updateorderdetailtbl] '11077','3','45.55','76','0.8'

select * from [Order Details]
where OrderID=11077