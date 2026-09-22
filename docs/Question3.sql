create database RaceDayDB;
use RaceDayDB;

create table Organiser(
OrganiserID integer not null,
Name varchar(50),
primary key (OrganiserID));

create table Participant(
ParticipantID integer not null,
Name varchar(50),
PhoneNumber varchar(10),
Age integer,
primary key (ParticipantID));

create table Category(
CategoryID integer not null,
Name varchar(50),
OrganiserID integer not null,
primary key(CategoryID),
foreign key(OrganiserID) references Organiser(OrganiserID));

create table Location(
LocationID integer not null,
Name varchar(50),
GPS_coordinates varchar(20),
StartingLineLocation varchar(20),
FinishingLineLocation varchar(20),
RouteLength decimal(10,2),
primary key(LocationID));

create table Event(
EventID integer not null,
Title varchar(50),
Date date not null,
StartTime time not null,
EndTime time not null,
CategoryID integer not null,
LocationID integer not null,
OrganiserID integer not null,
primary key(EventID),
foreign key(CategoryID) references Category(CategoryID),
foreign key(LocationID) references Location(LocationID),
foreign key(OrganiserID) references Organiser(OrganiserID));

create table Enrolment(
EnrolmentID integer not null,
ParticipantID integer not null,
EventID integer not null,
primary key(EnrolmentID),
foreign key (ParticipantID) references Participant(ParticipantID),
foreign key (EventID) references Event(EventID));

create table Results(
ResultID integer not null,
FinishingTime decimal(10,2),
FinishingPosition integer,
ParticipantID integer not null,
EventID integer not null,
OrganiserID integer not null,
primary key (ResultID),
foreign key (ParticipantID) references Participant(ParticipantID),
foreign key (EventID) references Event(EventID),
foreign key(OrganiserID) references Organiser(OrganiserID));
