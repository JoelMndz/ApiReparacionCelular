create database db_reparaciones;
go
use db_reparaciones
go
create table Rol(
	Id int identity(1,1) primary key,
	Nombre varchar(50) not null
)

create table Usuario(
	Id int identity(1,1) primary key,
	Nombre varchar(50) not null,
	Email varchar (50) not null,
	Telefono varchar(50) not null,
	Password varchar(max) not null,
	IdRol int,
	foreign key(IdRol) references Rol(Id)
)

create table Cliente(
	Id int identity(1,1) primary key,
	Nombre varchar(50) not null,
	Email varchar (50) not null,
	Telefono varchar(50) not null
)

create table Reparacion(
	Id int identity(1,1) primary key,
	FechaRegistro Datetime default getdate(),
	FechaEntrega Datetime,
	Estado varchar(50) check (Estado in ('PENDIENTE','PROGRESO','TERMINADO','RETIRADO')),
	IdCliente int,
	IdTecnico int,
	foreign key(IdCliente) references Cliente(Id),
	foreign key(IdTecnico) references Usuario(Id)
)

create table ReparacionDetalle(
	Id int identity(1,1) primary key,
	Descripcion varchar(100),
	Precio decimal(9,2),
	IdReparacion int,
	foreign key(IdReparacion) references Reparacion(Id)
)