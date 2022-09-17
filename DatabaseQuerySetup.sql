-- Create table 'Dieter'
DROP TABLE [dbo].[Dieter];
CREATE TABLE [dbo].[Dieter] (
	[DieterId] int IDENTITY (1,1) NOT NULL,
	[FirstName] nvarchar (50) NOT NULL,
	[LastName] nvarchar (50) NOT NULL,
	[UserId] nvarchar (100) NOT NULL
	PRIMARY KEY (DieterId)
);
GO

-- Create table 'MealList'
DROP TABLE [dbo].[MealList];
CREATE TABLE [dbo].[MealList] (
	[MealId] int IDENTITY (1,1) NOT NULL,
	[MealDate] date NOT NULL,
	[MealName] nvarchar (25) NOT NULL,
	[KiloJoule] int NOT NULL,
	[DieterId] int NOT NULL
	PRIMARY KEY (MealId),
	FOREIGN KEY (DieterId) REFERENCES Dieter (DieterId)
);
GO

-- Create table 'DietHistory'
DROP TABLE [dbo].[DietHistory];
CREATE TABLE [dbo].[DietHistory] (
	[RecordDate] date NOT NULL,
	[Weight] float NOT NULL,
	[DieterId] int NOT NULL
	PRIMARY KEY (RecordDate),
	FOREIGN KEY (DieterId) REFERENCES Dieter (DieterId)
);
GO

-- Create table 'Gym'
DROP TABLE [dbo].[Gym];
CREATE TABLE [dbo].[Gym] (
	[GymId] int IDENTITY (1,1) NOT NULL,
	[GymName] nvarchar (50) NOT NULL,
	[Longitude] int NOT NULL,
	[Latitude] int NOT NULL
	PRIMARY KEY (GymId)
);
GO

-- Create table 'Trainer'
DROP TABLE [dbo].[Trainer];
CREATE TABLE [dbo].[Trainer] (
	[TrainerId] int IDENTITY (1,1) NOT NULL,
	[FirstName] nvarchar (50) NOT NULL,
	[LastName] nvarchar (50) NOT NULL,
	[UserId] nvarchar (100) NOT NULL,
	[Tags] nvarchar (100) NOT NULL,
	[Description] nvarchar (max) NOT NULL,
	[GymId] int NOT NULL
	PRIMARY KEY (TrainerId),
	FOREIGN KEY (GymId) REFERENCES Gym (GymId)
);
GO

-- Create table 'Appointment'
DROP TABLE [dbo].[Appointment];
CREATE TABLE [dbo].[Appointment] (
	[AppointmentId] int IDENTITY (1,1) NOT NULL,
	[AppDate] datetime NOT NULL,
	[Length] int NOT NULL,
	[DieterId] int NOT NULL,
	[TrainerId] int NOT NULL
	PRIMARY KEY (AppointmentId, AppDate),
	FOREIGN KEY (DieterId) REFERENCES Dieter (DieterId),
	FOREIGN KEY (TrainerId) REFERENCES Trainer (TrainerId)
);
GO











