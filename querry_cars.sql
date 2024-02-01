create table if not exists roles
(
	id serial primary key,
	title varchar(255) not null
);

create table if not exists subscriptions
(
	id serial primary key,
	title varchar(255) not null
);

create table if not exists users
(
	id serial primary key,
	first_name varchar(255),
	last_name varchar(255),
	login varchar(50) not null,
	email varchar(255),
	phone varchar(255) not null,
	password varchar(32) not null,
	driver_pass varchar(12),
	role_id int not null,
	foreign key (role_id) references roles (id) on delete cascade on update cascade,
	sub_id int,
	foreign key (sub_id) references subscriptions (id) on delete cascade on update cascade
);

create table if not exists statuses
(
	id serial primary key,
	title varchar(50) not null
);

create table if not exists cars
(
	id serial primary key,
	brand varchar(50) not null,
	model varchar(100) not null,
	state_number varchar(9) not null,
	price money not null,
	latitude float not null,
	longitude float not null,
	status_id int not null,
	foreign key (status_id) references statuses (id)
);

create table if not exists reserved_cars
(
	id serial primary key,
	user_id int,
	car_id int not null,
	booking_status int not null,
	foreign key (user_id) references users (id),
	foreign key (car_id) references cars (id),
	foreign key (booking_status) references statuses (id)
);

insert into roles (title)
values
('admin'),
('user');

insert into users (first_name, last_name, phone, login, password, role_id)
values
('Райан', 'Гослинг', '+79533895130', 'admin', 'P@ssw0rd', 1),
('Валерий', 'Игнатов', '+79999999999', 'Valerous59', 'BigBang28', 2);

insert into subscriptions (title)
values
('default-user'),
('premium-user');

insert into statuses (title)
values
('Свободна'),
('Забронированна'),
('Занята')
