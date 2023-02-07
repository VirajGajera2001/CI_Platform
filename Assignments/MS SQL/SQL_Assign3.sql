create table Department(
	dept_id int not null,
	dept_name nvarchar(50) not null,
	primary key(dept_id)
)

create table Employee(
	emp_id int not null,
	dept_id int not null,
	mngr_id int not null,
	emp_name nvarchar(50) not null,
	salary int not null
)

insert into Department(dept_id,dept_name) values (10101,'chemical')

insert into Employee(emp_id,dept_id,mngr_id,emp_name,salary) values(201,10101,401,'amit',8373)

select * from Department

select * from Employee

/*query1 */
select D.dept_name,E.emp_name,E.salary
from Department D
inner join Employee E
on E.dept_id=D.dept_id
where E.salary in
	(select MAX(salary) from Employee group by dept_id)

/*query2 */
select dept_name
from Department 
inner join Employee
on Employee.dept_id=Department.dept_id
group by dept_name
having COUNT(emp_id)<3

/*query3 */
select D.dept_name as "Department Name",COUNT(E.emp_id) as "No. of Employee"
from Department D
inner join Employee E
on E.dept_id=D.dept_id
group by D.dept_name

/*query4 */
select D.dept_name as "Department Name",SUM(E.salary) as "total salary"
from Department D
inner join Employee E
on E.dept_id=D.dept_id
group by D.dept_name