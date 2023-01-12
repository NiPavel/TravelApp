drop Table Flights;

create Table Flights (
	Id int primary key,
	FromCountry nvarchar(50),
	ToCountry nvarchar(50),
	Date Datetime,
	Price float,
	PlaneId int,
	Seats int
);

select * from Flights;

drop Table Admins;
create table Admins(
	FirstName nvarchar(50),
	LastName nvarchar(50),
	Password nvarchar(50),
	Email nvarchar(50) primary key,
	Phone nvarchar(50)
);

select * from Admins;

drop Table Users;
create Table Users(
	FirstName nvarchar(50),
	LastName nvarchar(50),
	Password nvarchar(50),
	Email nvarchar(50) primary key,
	Phone nvarchar(50)
);

select * from Users;

drop Table Orders;
create Table Orders(
	Id int primary key,
	FlightId int,
	FName nvarchar(50),
	LName nvarchar(50),
	FromCountry nvarchar(50),
	ToCountry nvarchar(50),
	Email nvarchar(50),
	Date Datetime,
	Price float
);

select * from Orders;

drop Table Payments;
create Table Payments(
	Id int primary key,
	PayId int,
	PayFirstName nvarchar(50),
	PayLastName nvarchar(50),
	CardNumber bigint,
	Price float
);

select * from Payments;

drop Table Planes;
create Table Planes(
	PlaneId int primary key,
	PlaneType nvarchar(50),
	NumSeats int
);
select * From Planes;