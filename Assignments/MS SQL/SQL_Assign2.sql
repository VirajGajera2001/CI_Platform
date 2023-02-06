create table salesman(
	salesman_id int not null,
	name nvarchar(50) not null,
	city nvarchar(50) not null,
	commission int not null
)

create table customer(
	customer_id int not null,
	cust_name nvarchar(50) not null,
	city nvarchar(50) not null,
	grade int not null,
	salesman_id int not null
)

create table orders(
	ord_no int not null,
	purch_amt int not null,
	ord_date nvarchar(50) not null,
	customer_id int not null,
	salesman_id int not null
)

insert into salesman (salesman_id,name,city,commission) values (10001,'amit','junagadh',5)

insert into customer (customer_id,cust_name,city,grade,salesman_id) values (201,'tom','agra',1,10001)

insert into orders (ord_no,purch_amt,ord_date,customer_id,salesman_id) values (10,10000,'26 jan 2022',201,10001)

select * from salesman;

select * from customer;

select * from orders;

/* Query1 */
select name,cust_name,salesman.city
from salesman
inner join customer
on salesman.city=customer.city

/*Query2 */
select ord_no,purch_amt,customer.cust_name,customer.city
from orders
inner join customer
on customer.customer_id=orders.customer_id
where purch_amt between 500 and 2000

/*Query3 */
select C.cust_name as "Customer name",S.city,S.name as "Salesman",S.commission
from salesman S
inner join customer C
on S.salesman_id = C.salesman_id

/*Query4 */
select C.cust_name as "Customer Name",C.city as "Customer City", S.name as "Salesman",S.commission
from customer C
inner join salesman S
on S.salesman_id=C.salesman_id
where S.commission>12

/*Query5 */
select C.cust_name as "Customer Name",C.city as "Customer City", S.name as "Salesman",S.city as "Salesman city",S.commission
from customer C
inner join salesman S
on S.salesman_id=C.salesman_id
where S.commission>12
and S.city!=C.city

/*Query6 */
select O.ord_no,O.ord_date,O.purch_amt,C.cust_name as "Customer Name",C.grade,S.name as "Salesman",S.commission
from orders O
join customer C
join salesman S
on S.salesman_id=C.salesman_id
on S.salesman_id=O.salesman_id

/*Query7 */
select S.salesman_id,S.name as "Salesman",S.city as "Salesmancity",S.commission,C.customer_id,C.cust_name,C.city as "Customer city",C.grade,
O.ord_no,O.purch_amt,O.ord_date
from salesman S
join customer C
join orders O
on O.salesman_id=C.salesman_id
on O.salesman_id=S.salesman_id

/*Query8 */
select C.cust_name as "Customer Name",C.city as "Customer City",C.grade,S.name as "Salesman Name",S.city as "Salesman City"
from salesman S
join customer C
on S.salesman_id=C.salesman_id
order by C.customer_id desc

/*Query9 */
select C.cust_name,C.city as "Customer City",C.grade,S.name as "Salesman Name",S.city as "Salesman City"
from salesman S
join customer C
on S.salesman_id=C.salesman_id
where C.grade<300
order by C.customer_id desc

/*Query10 */
select C.cust_name,C.city,O.ord_no,O.ord_date,O.purch_amt
from customer C
join orders O
on C.customer_id=O.customer_id
order by O.ord_date asc

/*Query11 */
select C.cust_name,C.city as "Customer City",O.ord_no,O.ord_date,O.purch_amt,S.name as "Salesman Name",S.commission
from customer C
full outer join salesman S
on C.salesman_id=S.salesman_id
full outer join orders O
on C.customer_id=O.customer_id

/*Query12 */
select S.name as "Salesman Name"
from customer C
left outer join salesman S
on S.salesman_id=C.salesman_id
order by S.salesman_id asc

/*Query13 */
select S.name as "Salesman Name",C.cust_name,C.city as "Customer City",C.grade,O.ord_no,O.ord_date,O.purch_amt
from salesman S
full outer join customer C
full outer join orders O
On O.customer_id=C.customer_id
on O.salesman_id=S.salesman_id

/*Query14*/
select C.cust_name,O.ord_no,O.ord_date,O.purch_amt,S.name,C.grade
from customer C
right outer join salesman S
on C.salesman_id=S.salesman_id
left outer join orders O
on S.salesman_id=O.salesman_id
where O.purch_amt>2000
or C.grade is not null

/*Query15 */
select C.cust_name,O.ord_no,O.ord_date,O.purch_amt,S.name,C.grade
from customer C
right outer join salesman S
on C.salesman_id=S.salesman_id
left outer join orders O
on S.salesman_id=O.salesman_id
where O.purch_amt>2000
or C.grade is not null

/*Query16 */
select C.cust_name,C.city as "Customer City",O.ord_no,O.ord_date,O.purch_amt
from customer C
left outer join orders O
on C.customer_id=O.customer_id

/*Query17 */
select *
from salesman
cross join customer 

/*Query18 */
select *
from salesman S
cross join customer C
where  S.city=C.city

/*Query 19 */
select *
from salesman S
cross join customer C
where  S.city is not null
and C.grade is not null

/*Query 20 */
select *
from salesman S
cross join customer C
where S.city!=C.city
and C.grade is not null