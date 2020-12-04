drop table [dbo].[Departments]
drop table [dbo].[Clients]
drop table [dbo].[Accounts]
drop table [dbo].[Deposites]

create table [dbo].[Departments]
(
	[Id] int not null primary key identity, 
	[ParentId] int not null,
	[Name] nvarchar(50)
)

create table [dbo].[Clients]
(
	[Id] int not null primary key identity, 
	[DepartmentId] int not null, 
	[Name] nvarchar(50) not null
)

create table [dbo].[Accounts]
(
	[Id] int not null primary key identity,
	[ClientId] int not null,
	[Balance] decimal (10, 4) not null, 
	[CreateDate] datetime not null
)

create table [dbo].[Deposites]
(
	[Id] int not null primary key identity,
	[ClientId] int not null,
	[Name] nvarchar(50),
	[Balance] decimal(10,4),
	[CreateDate] datetime not null,
	[IsWithCapitalization] bit not null
)


set identity_insert [dbo].[Departments] on;

insert into [dbo].[Departments] (Id, ParentId, [Name]) 
values (1, 0, N'Департамент_01'),
(2, 0, N'Департамент_02'),
(3, 1, N'Департамент_03');

set identity_insert [dbo].[Departments] off;

--select * from [dbo].[Departments]

