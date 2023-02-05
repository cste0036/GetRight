-- Create table 'Dieter'
DROP TABLE [dbo].[Dieter];
CREATE TABLE [dbo].[Dieter] (
	[DieterId] int IDENTITY (1,1) NOT NULL,
	[FirstName] nvarchar (50),
	[LastName] nvarchar (50),
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
	[Latitude] NUMERIC(10, 8) NOT NULL CHECK (Latitude >= -90 AND Latitude <= 90),
	[Longitude] NUMERIC(11, 8) NOT NULL CHECK (Longitude >= -180 AND Longitude <= 180)
	PRIMARY KEY (GymId)
);
GO

INSERT INTO [dbo].[Gym] ([GymName], [Latitude], [Longitude]) VALUES 
(N'BigSwoleTimes', CAST(-37.87682300 AS Decimal(10, 8)), CAST(145.04583700 AS Decimal(11, 8)));
INSERT INTO [dbo].[Gym] ([GymName], [Latitude], [Longitude]) VALUES 
(N'LiftTime', CAST(-37.91500000 AS Decimal(10, 8)), CAST(145.13000000 AS Decimal(11, 8)));


-- Create table 'Trainer'
DROP TABLE [dbo].[Trainer];
CREATE TABLE [dbo].[Trainer] (
	[TrainerId] int IDENTITY (1,1) NOT NULL,
	[FirstName] nvarchar (50),
	[LastName] nvarchar (50),
	[UserId] nvarchar (100) NOT NULL,
	[Tags] nvarchar (100),
	[Description] nvarchar (max),
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

-- Create table 'Rating'
DROP TABLE [dbo].[Rating];
CREATE TABLE [dbo].[Rating] (
	[DieterId] int NOT NULL,
	[TrainerId] int NOT NULL,
	rating int NOT NULL CHECK(NumericField BETWEEN 1 AND 5)
	PRIMARY KEY (DieterId, TrainerId),
	FOREIGN KEY (DieterId) REFERENCES Dieter (DieterId),
	FOREIGN KEY (TrainerId) REFERENCES Trainer (TrainerId)
);
GO

-- Create table 'Newsletter'
DROP TABLE [dbo].[Newsletter];
CREATE TABLE [dbo].[Newsletter]
(
 [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
 [Path] VARCHAR(50) NOT NULL,
 [Name] VARCHAR(50) NOT NULL
);
GO