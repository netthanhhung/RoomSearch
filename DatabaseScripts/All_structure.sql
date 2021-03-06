USE [master]
GO
/****** Object:  Database [RoomSearch]    Script Date: 10/02/2015 16:28:14 ******/
CREATE DATABASE [RoomSearch] ON  PRIMARY 
( NAME = N'RoomSearch_Empty', FILENAME = N'E:\Chanh\Database\RoomSearch_Empty.mdf' , SIZE = 156672KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RoomSearch_Empty_log', FILENAME = N'E:\Chanh\Database\RoomSearch_Empty_log.ldf' , SIZE = 199296KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [RoomSearch] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RoomSearch].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RoomSearch] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [RoomSearch] SET ANSI_NULLS OFF
GO
ALTER DATABASE [RoomSearch] SET ANSI_PADDING OFF
GO
ALTER DATABASE [RoomSearch] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [RoomSearch] SET ARITHABORT OFF
GO
ALTER DATABASE [RoomSearch] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [RoomSearch] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [RoomSearch] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [RoomSearch] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [RoomSearch] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [RoomSearch] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [RoomSearch] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [RoomSearch] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [RoomSearch] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [RoomSearch] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [RoomSearch] SET  DISABLE_BROKER
GO
ALTER DATABASE [RoomSearch] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [RoomSearch] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [RoomSearch] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [RoomSearch] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [RoomSearch] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [RoomSearch] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [RoomSearch] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [RoomSearch] SET  READ_WRITE
GO
ALTER DATABASE [RoomSearch] SET RECOVERY FULL
GO
ALTER DATABASE [RoomSearch] SET  MULTI_USER
GO
ALTER DATABASE [RoomSearch] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [RoomSearch] SET DB_CHAINING OFF
GO
USE [RoomSearch]
GO
/****** Object:  UserDefinedFunction [dbo].[fuChuyenCoDauThanhKhongDau]    Script Date: 10/02/2015 16:28:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fuChuyenCoDauThanhKhongDau]
(
      @strInput NVARCHAR(4000)
)
RETURNS NVARCHAR(4000)
AS
BEGIN   
    IF @strInput IS NULL RETURN @strInput
    IF @strInput = '' RETURN @strInput
    DECLARE @RT NVARCHAR(4000)
    DECLARE @SIGN_CHARS NCHAR(136)
    DECLARE @UNSIGN_CHARS NCHAR (136)
    SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế
                  ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý
                  ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ
                  ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ'
                  +NCHAR(272)+ NCHAR(208)
    SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee
                  iiiiiooooooooooooooouuuuuuuuuuyyyyy
                  AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII
                  OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'
    DECLARE @COUNTER int
    DECLARE @COUNTER1 int
    SET @COUNTER = 1
    WHILE (@COUNTER <=LEN(@strInput))
    BEGIN 
      SET @COUNTER1 = 1
      --Tìm trong chuỗi mẫu
       WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1)
       BEGIN
     IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1))
            = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) )
     BEGIN         
          IF @COUNTER=1
              SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1)
              + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) 
          ELSE
              SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1)
              +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1)
              + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER)
              BREAK
               END
             SET @COUNTER1 = @COUNTER1 +1
       END
      --Tìm tiếp
       SET @COUNTER = @COUNTER +1
    END
    SET @strInput = replace(@strInput,' ','-')
    RETURN @strInput
END
GO
/****** Object:  Table [dbo].[ImageType]    Script Date: 10/02/2015 16:28:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ImageType](
	[ImageTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_ImageType] PRIMARY KEY CLUSTERED 
(
	[ImageTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PostType]    Script Date: 10/02/2015 16:28:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PostType](
	[PostTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_PostType] PRIMARY KEY CLUSTERED 
(
	[PostTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Country]    Script Date: 10/02/2015 16:28:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Concurrency] [timestamp] NULL,
	[DateCreated] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[CreatedBy] [varchar](128) NULL,
	[UpdatedBy] [varchar](128) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContactType]    Script Date: 10/02/2015 16:28:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContactType](
	[ContactTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED 
(
	[ContactTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[ufnConvertToUnsignString]    Script Date: 10/02/2015 16:28:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ufnConvertToUnsignString]
(
      @strInput NVARCHAR(max)
)
RETURNS NVARCHAR(max)
AS
BEGIN   
    IF @strInput IS NULL RETURN @strInput
    IF @strInput = '' RETURN @strInput
    DECLARE @RT NVARCHAR(4000)
    DECLARE @SIGN_CHARS NCHAR(136)
    DECLARE @UNSIGN_CHARS NCHAR (136)
   	SET @strInput = LTRIM(@strInput);
	SET @strInput = LOWER(RTRIM(@strInput));

    SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế
                  ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý
                  ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ
                  ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ'
                  +NCHAR(272)+ NCHAR(208)
    SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee
                  iiiiiooooooooooooooouuuuuuuuuuyyyyy
                  AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII
                  OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'
    DECLARE @COUNTER int
    DECLARE @COUNTER1 int
    SET @COUNTER = 1
    WHILE (@COUNTER <=LEN(@strInput))
    BEGIN 
      SET @COUNTER1 = 1
      --Tìm trong chuỗi mẫu
       WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1)
       BEGIN
     IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1))
            = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) )
     BEGIN         
          IF @COUNTER=1
              SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1)
              + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) 
          ELSE
              SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1)
              +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1)
              + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER)
              BREAK
               END
             SET @COUNTER1 = @COUNTER1 +1
       END
      --Tìm tiếp
       SET @COUNTER = @COUNTER +1
    END
    --SET @strInput = replace(@strInput,' ','-')
    RETURN @strInput
END
GO
/****** Object:  Table [dbo].[RoomType]    Script Date: 10/02/2015 16:28:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoomType](
	[RoomTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_RoomType] PRIMARY KEY CLUSTERED 
(
	[RoomTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RealestateType]    Script Date: 10/02/2015 16:28:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RealestateType](
	[RealestateTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_RealestateType] PRIMARY KEY CLUSTERED 
(
	[RealestateTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[procListRoomType]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListRoomType]	

AS
BEGIN
SET NOCOUNT ON

SELECT	RE.RoomTypeId, RE.Name, RE.Description, 
RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
FROM	RoomType RE

END
GO
/****** Object:  StoredProcedure [dbo].[procListRealestateType]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListRealestateType]	

AS
BEGIN
SET NOCOUNT ON

SELECT	RE.RealestateTypeId, RE.Name, RE.Description, 
RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
FROM	RealestateType RE

END
GO
/****** Object:  StoredProcedure [dbo].[procListPostType]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListPostType]	

AS
BEGIN
SET NOCOUNT ON

SELECT	RE.PostTypeId, RE.Name, 
RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
FROM	PostType RE

END
GO
/****** Object:  StoredProcedure [dbo].[procSaveRoomType]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveRoomType]

@RoomTypeId int OUTPUT,
@Name varchar(128),
@Description varchar(128),
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

IF @RoomTypeId IS NULL
BEGIN

INSERT INTO RoomType(
Name,
Description,	
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@Name,
@Description,
GETDATE(),
GETDATE(),
@CurrentUser,
@CurrentUser
)

SET @RoomTypeId = SCOPE_IDENTITY()

END ELSE BEGIN

UPDATE RoomType SET
Name = @Name,
Description = @Description,	
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE RoomTypeId = @RoomTypeId

END

SELECT Concurrency FROM RoomType WHERE RoomTypeId = @RoomTypeId
END
GO
/****** Object:  StoredProcedure [dbo].[procListCountry]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListCountry] (@CountryId int = NULL)

AS
BEGIN
SET NOCOUNT ON

SELECT CountryID, Name, Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
FROM Country
WHERE (@CountryID IS NULL OR CountryId = @CountryId)
END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteCountry]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteCountry](@CountryId int)	
AS
BEGIN
SET NOCOUNT ON

DELETE Country WHERE CountryId = @CountryId
END
GO
/****** Object:  Table [dbo].[ContactInformation]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[ContactInformation](
	[ContactInformationId] [int] IDENTITY(1,1) NOT NULL,
	[ContactTypeId] [int] NOT NULL,
	[FirstName] [varchar](128) NULL,
	[LastName] [varchar](128) NULL,
	[Address] [varchar](255) NULL,
	[Address2] [varchar](255) NULL,
	[District] [varchar](255) NULL,
	[DistrictId] [int] NULL,
	[City] [varchar](255) NULL,
	[CityId] [int] NULL,
	[State] [varchar](255) NULL,
	[Postcode] [varchar](50) NULL,
	[CountryId] [int] NULL,
	[PhoneNumber] [varchar](128) NULL,
	[FaxNumber] [varchar](128) NULL,
	[Email] [varchar](255) NULL,
	[DoB] [datetime] NULL,
	[Visa] [varchar](128) NULL,
	[VisaValidFrom] [datetime] NULL,
	[VisaValidTo] [datetime] NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_ContactInformation] PRIMARY KEY CLUSTERED 
(
	[ContactInformationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[City]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Concurrency] [timestamp] NULL,
	[DateCreated] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[CreatedBy] [varchar](128) NULL,
	[UpdatedBy] [varchar](128) NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Post]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Post](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[PostTypeId] [int] NOT NULL,
	[PersonName] [nvarchar](200) NOT NULL,
	[PhoneNumber] [varchar](128) NULL,
	[Email] [varchar](255) NULL,
	[Gender] [int] NULL,
	[RoomTypeId] [int] NULL,
	[RealestateTypeId] [int] NULL,
	[AvailableRooms] [int] NULL,
	[Description] [nvarchar](max) NOT NULL,
	[MeterSquare] [decimal](20, 8) NULL,
	[Floor] [int] NULL,
	[Address] [nvarchar](255) NULL,
	[DistrictId] [int] NULL,
	[CityId] [int] NULL,
	[CountryId] [int] NULL,
	[Price] [decimal](20, 8) NULL,
	[IsLegacy] [bit] NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
	[SearchKeyWords] [nvarchar](max) NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Image]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Image](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[ImageTypeId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[FileName] [varchar](128) NULL,
	[ImageContent] [varbinary](max) NULL,
	[ImageSmallContent] [varbinary](max) NULL,
	[DisplayIndex] [int] NULL,
	[Description] [varchar](max) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[procDeleteImage]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteImage](@ImageId int)	
AS
BEGIN
SET NOCOUNT ON

DELETE Image WHERE ImageId = @ImageId
END
GO
/****** Object:  Table [dbo].[District]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[District](
	[DistrictId] [int] IDENTITY(1,1) NOT NULL,
	[CityId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Concurrency] [timestamp] NULL,
	[DateCreated] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[CreatedBy] [varchar](128) NULL,
	[UpdatedBy] [varchar](128) NULL,
 CONSTRAINT [PK_District] PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[dbo.[procSearchPost]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dbo.[procSearchPost]	
@PostTypeId int,
@RoomTypeId int = null,
@CountryId int = null,
@CityId int = null,
@DistrictId int = null,
@PersonName varchar(128) = null,
@PhoneNumber varchar(128) = NULL,
@Email varchar(255) = NULL,		
@PriceFrom decimal(20,8),	
@PriceTo decimal(20,8) = NULL,	
@DateFrom datetime,
@DateTo datetime = NULL,
@MeterSquareFrom decimal(20,8) = NULL,
@MeterSquareTo decimal(20,8) = NULL,	
@ShowLegacy bit
AS
BEGIN
SET NOCOUNT ON

	SELECT	
		P.PostId
      ,P.PostTypeId
      ,P.PersonName
      ,P.PhoneNumber
      ,P.Email
      ,P.RoomTypeId
      ,P.Description
      ,P.MeterSquare
      ,P.Floor
      ,P.Address
      ,P.DistrictId
      ,P.CityId
      ,P.CountryId
      ,P.Price
      ,P.IsLegacy
      ,P.Concurrency
      ,P.DateCreated
      ,P.DateUpdated
      ,P.CreatedBy
      ,P.UpdatedBy
	FROM	dbo.Post P
	INNER JOIN dbo.RoomType RT on P.RoomTypeId = RT.RoomTypeId
	WHERE	P.PostTypeId = @PostTypeId
	AND	(@RoomTypeId IS NULL OR P.RoomTypeId = @RoomTypeId)
	AND	(@CountryId is null OR P.CountryId = @CountryId)
	AND	(@CityId is null OR P.CityId = @CityId)
	AND	(@DistrictId is null OR P.DistrictId = @DistrictId)
	AND	(@PersonName IS NULL OR (P.PersonName like '%' + @PersonName + '%'))
	AND	(@PhoneNumber IS NULL OR P.PhoneNumber = @PhoneNumber)
	AND	(@Email IS NULL OR P.Email = @Email)
	AND (@PriceFrom IS NULL OR P.Price IS NULL OR P.Price >= @PriceFrom)
	AND (@PriceTo IS NULL OR P.Price IS NULL OR P.Price <= @PriceTo)
	AND (P.DateUpdated >= @DateFrom)
	AND (@DateTo IS NULL OR P.DateUpdated <= @DateTo)
	AND (@MeterSquareFrom IS NULL OR P.MeterSquare IS NULL OR P.MeterSquare >= @MeterSquareFrom) 
	AND (@MeterSquareTo IS NULL OR P.MeterSquare IS NULL OR P.MeterSquare <= @MeterSquareTo)
	AND (@ShowLegacy = 1 OR P.IsLegacy = 0)

END
GO
/****** Object:  StoredProcedure [dbo].[procCountNoPricePost]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procCountNoPricePost]	
AS
BEGIN
SET NOCOUNT ON
		
	SELECT	COUNT(*)
    FROM	dbo.Post P
    WHERE P.DateUpdated > DATEADD(DD, -7, GETDATE())
    AND		P.Price IS NULL
END
GO
/****** Object:  StoredProcedure [dbo].[procCleanUpDatabase]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procCleanUpDatabase]
AS
BEGIN
     
	DELETE FROM Post WHERE DateCreated < DATEADD(MM, -1, Getdate())     
	DELETE FROM Image WHERE DateCreated < DATEADD(MM, -1, Getdate())
	
    DBCC SHRINKDATABASE ('RoomSearch', 10);
         
END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteContactInformation]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteContactInformation](@ContactInformationId int)	
AS
BEGIN
SET NOCOUNT ON

DELETE ContactInformation WHERE ContactInformationId = @ContactInformationId
END
GO
/****** Object:  StoredProcedure [dbo].[procListContactInformation]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListContactInformation] (
@ContactInformationId int = NULL
)

AS
BEGIN
	SET NOCOUNT ON

	SELECT ContactInformationID, ContactTypeId, FirstName, LastName, Address, Address2, District, DistrictId, City, CityId, State, Postcode, CountryId,
		PhoneNumber, FaxNumber, Email, DoB, Visa, VisaValidFrom, VisaValidTo,
		Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
	FROM ContactInformation
	WHERE (@ContactInformationID IS NULL OR ContactInformationId = @ContactInformationId)
END
GO
/****** Object:  StoredProcedure [dbo].[procListCity]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListCity] 
(
	@CountryId int = NULL
	, @CityId int = NULL
)

AS
BEGIN
	SET NOCOUNT ON

	SELECT CityID, CountryId, Name, Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
	FROM City
	WHERE (@CountryId IS NULL OR CountryId = @CountryId)
	AND (@CityID IS NULL OR CityId = @CityId)
END
GO
/****** Object:  StoredProcedure [dbo].[procSavePost]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object: StoredProcedure [dbo].[procSavePost1] Script Date: 04/03/2012 14:43:56 ******/
CREATE PROCEDURE [dbo].[procSavePost]
@PostID int OUTPUT,
@PostTypeId [int],
@PersonName [nvarchar](200),
@PhoneNumber [varchar](128),
@Email [varchar](255),	
@Gender int,		
@RoomTypeId [int],
@RealestateTypeId [int],
@AvailableRooms int,
@Description [nvarchar](max),
@MeterSquare [decimal](20, 8),
@Floor [int],
@Address [nvarchar](255),
@DistrictId [int],
@CityId [int],
@CountryId [int],		
@Price [decimal](20, 8),
@IsLegacy bit,
@CurrentUser varchar(128)
AS
BEGIN
	SET NOCOUNT ON

	IF @PostID IS NULL BEGIN

		INSERT INTO Post(
			[PostTypeId],
			[PersonName],
			[PhoneNumber],
			[Email],
			[Gender],
			[RoomTypeId],
			[RealestateTypeId],
			AvailableRooms,
			[Description],
			[SearchKeyWords],
			[MeterSquare],
			[Floor],
			[Address],
			[DistrictId],
			[CityId],
			[CountryId],		
			[Price],
			[IsLegacy],
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@PostTypeId,
			@PersonName,
			@PhoneNumber,
			@Email,
			@Gender,
			@RoomTypeId,
			@RealestateTypeId,
			@AvailableRooms,
			@Description,
			(dbo.[ufnConvertToUnsignString](@Address) + ' ' + dbo.[ufnConvertToUnsignString](@Description)),
			@MeterSquare,
			@Floor,
			@Address,
			@DistrictId,
			@CityId,
			@CountryId,		
			@Price,
			@IsLegacy,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)
		
		SET @PostID = SCOPE_IDENTITY()

		END ELSE BEGIN

		UPDATE Post SET

			PostTypeId = @PostTypeId,
			PersonName = @PersonName,
			PhoneNumber = @PhoneNumber,
			Email = @Email,
			Gender = @Gender,
			RoomTypeId = @RoomTypeId,
			RealestateTypeId = @RealestateTypeId,
			AvailableRooms = @AvailableRooms,
			Description = @Description,
			SearchKeywords = (dbo.[ufnConvertToUnsignString](@Address) + ' ' + dbo.[ufnConvertToUnsignString](@Description)),
			MeterSquare = @MeterSquare,
			Floor = @Floor,
			Address = @Address,
			DistrictId = @DistrictId,
			CityId = @CityId,
			CountryId = @CountryId,		
			Price = @Price,
			IsLegacy = @IsLegacy,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE PostID = @PostID

	END

	SELECT Concurrency FROM Post WHERE PostID = @PostID

	-- ======================================================================
	-- Change History
	-- ======================================================================
	-- 23-Jul-2014 HT
	-- INIT
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveImage]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveImage]

@ImageID int OUTPUT,
@ImageTypeId int,
@ItemId int,
@FileName varchar(128),
@ImageContent varbinary(MAX),
@ImageSmallContent varbinary(MAX),
@DisplayIndex int,
@Description varchar(max),
@CurrentUser varchar(128)
AS

SET NOCOUNT ON
BEGIN
IF (NOT EXISTS (SELECT * FROM Image WHERE ImageID = @ImageID)) BEGIN

INSERT INTO Image(
ImageTypeId
,ItemId
,[FileName]
,[ImageContent]
,[ImageSmallContent]
,[DisplayIndex]
,[Description]
,[DateCreated]
,[DateUpdated]
,[CreatedBy]
,[UpdatedBy]
) VALUES (
@ImageTypeId
,@ItemId
,@FileName
,@ImageContent
,@ImageSmallContent
,@DisplayIndex
,@Description
,GETDATE()
,GETDATE()
,@CurrentUser
,@CurrentUser
)

SET @ImageID = SCOPE_IDENTITY()

END ELSE BEGIN

UPDATE Image SET

ImageTypeId = @ImageTypeId
,ItemId = @ItemId
,[FileName] = @FileName
,[ImageContent] = @ImageContent	
,[ImageSmallContent] = @ImageSmallContent
,[DisplayIndex] = @DisplayIndex
,[Description] = @Description
,DateUpdated = GETDATE()
,UpdatedBy = @CurrentUser

WHERE (ImageId = @ImageID)

END

SELECT Concurrency FROM Image WHERE ImageId = @ImageID
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveContactInformation]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveContactInformation]

@ContactInformationID int OUTPUT
,@ContactTypeId int
,@FirstName varchar(128)
,@LastName varchar(128)
,@Address varchar(255)
,@Address2 varchar(255)
,@District varchar(255)
,@DistrictId int
,@City varchar(255)
,@CityId int
,@State varchar(255)
,@Postcode varchar(255)
,@CountryId int
,@PhoneNumber varchar(128)
,@FaxNumber varchar(128)
,@Email varchar(255)
,@DoB datetime
,@Visa varchar(128)
,@VisaValidFrom datetime
,@VisaValidTo datetime
,@CurrentUser varchar(255)
AS
BEGIN
	SET NOCOUNT ON

	IF @ContactInformationID IS NULL BEGIN

	INSERT INTO ContactInformation(
			[ContactTypeId]
			,[FirstName]
			,[LastName]
			,[Address]
			,[Address2]
			,[District]
			,[DistrictId]
			,[City]
			,[CityId]
			,[State]
			,[Postcode]
			,[CountryId]
			,[PhoneNumber]
			,[FaxNumber]
			,[Email]
			,DoB
			,Visa
			,VisaValidFrom
			,VisaValidTo
			,DateCreated
			,DateUpdated
			,CreatedBy
			,UpdatedBy
		) VALUES (
			@ContactTypeId
			,@FirstName
			,@LastName
			,@Address
			,@Address2
			,@District
			,@DistrictId
			,@City
			,@CityId
			,@State
			,@Postcode
			,@CountryId
			,@PhoneNumber
			,@FaxNumber
			,@Email
			,@DoB
			,@Visa
			,@VisaValidFrom
			,@VisaValidTo
			,GETDATE()
			,GETDATE()
			,@CurrentUser
			,@CurrentUser
		)

		SET @ContactInformationID = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE ContactInformation SET
			ContactTypeId = @ContactTypeId,
			FirstName = @FirstName,
			LastName = @LastName,
			Address = @Address,
			Address2 = @Address2 ,
			District = @District,
			DistrictId = @DistrictId,
			City = @City ,
			CityId = @CityId ,
			State = @State ,
			Postcode = @Postcode ,
			CountryID = @CountryID ,
			PhoneNumber = @PhoneNumber ,
			FaxNumber = @FaxNumber ,
			Email = @Email,
			DoB = @DoB,
			Visa = @Visa,
			VisaValidFrom = @VisaValidFrom,
			VisaValidTo = @VisaValidTo,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE ContactInformationID = @ContactInformationID

	END

	SELECT Concurrency FROM ContactInformation WHERE ContactInformationID = @ContactInformationID
END
GO
/****** Object:  StoredProcedure [dbo].[procListImage]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListImage]
(
@ImageId int = NULL
,@ItemId int = NULL
,@ImageTypeId int = NULL
,@LoadType int -- 0 : no content, 1 : load big contect, 2 : load small conten, 3 : load all
)

AS
BEGIN
SET NOCOUNT ON

SELECT ImageID, ImageTypeId, ItemId, FileName,
CASE WHEN (@LoadType = 1 OR @LoadType = 3) THEN I.ImageContent ELSE NULL END AS ImageContent,
CASE WHEN (@LoadType = 2 OR @LoadType = 3) THEN I.ImageSmallContent ELSE NULL END AS ImageSmallContent,
DisplayIndex, Description,
Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
FROM dbo.[Image] I
WHERE (@ImageID IS NULL OR ImageId = @ImageId)
AND	(@ItemId IS NULL OR ItemId = @ItemId)
AND (@ImageTypeId IS NULL OR ImageTypeId = @ImageTypeId)
END
GO
/****** Object:  StoredProcedure [dbo].[procSearchPost]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSearchPost]	
@PostTypeId int,
@RoomTypeId int = null,
@RealestateTypeId int = null,
@CountryId int = null,
@CityId int = null,
@DistrictId int = null,
@PersonName nvarchar(128) = null,
@PhoneNumber varchar(128) = NULL,
@Email varchar(255) = NULL,		
@Gender int = NULL,
@PriceFrom decimal(20,8) = NULL,	
@PriceTo decimal(20,8) = NULL,	
@DateFrom datetime  = NULL,
@DateTo datetime = NULL,
@MeterSquareFrom decimal(20,8) = NULL,
@MeterSquareTo decimal(20,8) = NULL,	
@ShowLegacy bit
AS
BEGIN
SET NOCOUNT ON

	SELECT	
		P.PostId
      ,P.PostTypeId
      ,P.PersonName
      ,P.PhoneNumber
      ,P.Email
      ,P.Gender
      ,P.RoomTypeId
      ,P.RealestateTypeId
      ,RLT.Name as RealestateType
      ,RT.Name as RoomType
      ,P.AvailableRooms
      ,P.Description
      ,P.MeterSquare
      ,P.Floor
      ,P.Address
      ,P.DistrictId
      ,D.Name as District
      ,P.CityId
      ,CT.Name as City
      ,P.CountryId
      ,C.Name as Country
      ,P.Price
      ,P.IsLegacy
      ,P.Concurrency
      ,P.DateCreated
      ,P.DateUpdated
      ,P.CreatedBy
      ,P.UpdatedBy
	FROM	dbo.Post P
	LEFT OUTER JOIN dbo.RoomType RT on P.RoomTypeId = RT.RoomTypeId
	LEFT OUTER JOIN dbo.RealestateType RLT on P.RealestateTypeId = RLT.RealestateTypeId
	LEFT OUTER JOIN dbo.Country C on P.CountryId = C.CountryId
	LEFT OUTER JOIN dbo.City CT on P.CityId = CT.CityId
	LEFT OUTER JOIN dbo.District D on P.DistrictId = D.DistrictId
	WHERE	P.PostTypeId = @PostTypeId
	AND	(@RoomTypeId IS NULL OR P.RoomTypeId = @RoomTypeId)
	AND	(@RealestateTypeId IS NULL OR P.RealestateTypeId = @RealestateTypeId)
	AND	(@CountryId is null OR P.CountryId = @CountryId)
	AND	(@CityId is null OR P.CityId = @CityId)
	AND	(@DistrictId is null OR P.DistrictId = @DistrictId)
	AND	(@PersonName IS NULL OR (P.PersonName like '%' + @PersonName + '%'))
	AND	(@PhoneNumber IS NULL OR P.PhoneNumber = @PhoneNumber)
	AND	(@Email IS NULL OR P.Email = @Email)
	AND	(@Gender IS NULL OR P.Gender = @Gender)
	AND (@PriceFrom IS NULL OR P.Price IS NULL OR P.Price >= @PriceFrom)
	AND (@PriceTo IS NULL OR P.Price IS NULL OR P.Price <= @PriceTo)
	AND (P.DateUpdated >= @DateFrom)
	AND (@DateTo IS NULL OR P.DateUpdated <= @DateTo)
	AND (@MeterSquareFrom IS NULL OR P.MeterSquare IS NULL OR P.MeterSquare >= @MeterSquareFrom) 
	AND (@MeterSquareTo IS NULL OR P.MeterSquare IS NULL OR P.MeterSquare <= @MeterSquareTo)
	AND (@ShowLegacy = 1 OR P.IsLegacy = 0)

END
GO
/****** Object:  StoredProcedure [dbo].[procListDistrict]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListDistrict] 
(
	@CityId int = NULL
	, @DistrictId int = NULL
)

AS
BEGIN
	SET NOCOUNT ON

	SELECT DistrictID, CityId, Name, Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
	FROM District
	WHERE (@CityId IS NULL OR CityId = @CityId)
	AND (@DistrictID IS NULL OR DistrictId = @DistrictId)
	order by CityId, Name
END
GO
/****** Object:  StoredProcedure [dbo].[procGetPost]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procGetPost]	
	@PostId int
AS
BEGIN
SET NOCOUNT ON

	SELECT	
		P.PostId
      ,P.PostTypeId
      ,P.PersonName
      ,P.PhoneNumber
      ,P.Email
      ,P.Gender
      ,P.RoomTypeId
      ,P.RealestateTypeId
      ,RLT.Name as RealestateType
      ,RT.Name as RoomType
      ,P.AvailableRooms
      ,P.Description
      ,P.MeterSquare
      ,P.Floor
      ,P.Address
      ,P.DistrictId
      ,D.Name as District
      ,P.CityId
      ,CT.Name as City
      ,P.CountryId
      ,C.Name as Country
      ,P.Price
      ,P.IsLegacy
      ,P.Concurrency
      ,P.DateCreated
      ,P.DateUpdated
      ,P.CreatedBy
      ,P.UpdatedBy
	FROM	dbo.Post P
	LEFT OUTER JOIN dbo.RoomType RT on P.RoomTypeId = RT.RoomTypeId
	LEFT OUTER JOIN dbo.RealestateType RLT on P.RealestateTypeId = RLT.RealestateTypeId
	LEFT OUTER JOIN dbo.Country C on P.CountryId = C.CountryId
	LEFT OUTER JOIN dbo.City CT on P.CityId = CT.CityId
	LEFT OUTER JOIN dbo.District D on P.DistrictId = D.DistrictId
	WHERE	P.PostId = @PostId
	

END
GO
/****** Object:  StoredProcedure [dbo].[procCountPost]    Script Date: 10/02/2015 16:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procCountPost]	
@PostTypeId int,
@RoomTypeId int = null,
@RealestateTypeId int = null,
@CountryId int = null,
@CityId int = null,
@DistrictId int = null,
@PersonName nvarchar(128) = null,
@PhoneNumber varchar(128) = NULL,
@Email varchar(255) = NULL,		
@Gender int = NULL,
@PriceFrom decimal(20,8) = NULL,	
@PriceTo decimal(20,8) = NULL,	
@DateFrom datetime  = NULL,
@DateTo datetime = NULL,
@MeterSquareFrom decimal(20,8) = NULL,
@MeterSquareTo decimal(20,8) = NULL,
@Keywords varchar(255) = NULL,	
@ShowLegacy bit
AS
BEGIN
SET NOCOUNT ON
		
	SELECT	
		COUNT(*)
	FROM	dbo.Post P
	LEFT OUTER JOIN dbo.RoomType RT on P.RoomTypeId = RT.RoomTypeId
	LEFT OUTER JOIN dbo.RealestateType RLT on P.RealestateTypeId = RLT.RealestateTypeId
	LEFT OUTER JOIN dbo.Country C on P.CountryId = C.CountryId
	LEFT OUTER JOIN dbo.City CT on P.CityId = CT.CityId
	LEFT OUTER JOIN dbo.District D on P.DistrictId = D.DistrictId
	WHERE	P.PostTypeId = @PostTypeId
	AND	(@RoomTypeId IS NULL OR P.RoomTypeId = @RoomTypeId)
	AND	(@RealestateTypeId IS NULL OR P.RealestateTypeId = @RealestateTypeId)
	AND	(@CountryId is null OR P.CountryId = @CountryId)
	AND	(@CityId is null OR P.CityId = @CityId)
	AND	(@DistrictId is null OR P.DistrictId = @DistrictId)
	AND	(@PersonName IS NULL OR (P.PersonName like '%' + @PersonName + '%'))
	AND	(@PhoneNumber IS NULL OR P.PhoneNumber = @PhoneNumber)
	AND	(@Email IS NULL OR P.Email = @Email)
	AND	(@Gender IS NULL OR P.Gender = @Gender)
	AND (@PriceFrom IS NULL OR P.Price IS NULL OR P.Price >= @PriceFrom)
	AND (@PriceTo IS NULL OR P.Price IS NULL OR P.Price <= @PriceTo)
	AND (P.DateUpdated >= @DateFrom)
	AND (@DateTo IS NULL OR P.DateUpdated <= @DateTo)
	AND (@MeterSquareFrom IS NULL OR P.MeterSquare IS NULL OR P.MeterSquare >= @MeterSquareFrom) 
	AND (@MeterSquareTo IS NULL OR P.MeterSquare IS NULL OR P.MeterSquare <= @MeterSquareTo)
	ANd (@Keywords IS NULL OR SearchKeyWords like '%' + dbo.[ufnConvertToUnsignString](@Keywords) + '%')
	AND (@ShowLegacy = 1 OR P.IsLegacy = 0)

END
GO
/****** Object:  Default [DF_ImageType_DateCreated]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[ImageType] ADD  CONSTRAINT [DF_ImageType_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_ImageType_DateUpdated]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[ImageType] ADD  CONSTRAINT [DF_ImageType_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_ImageType_CreatedBy]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[ImageType] ADD  CONSTRAINT [DF_ImageType_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_ImageType_UpdatedBy]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[ImageType] ADD  CONSTRAINT [DF_ImageType_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_PostType_DateCreated]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[PostType] ADD  CONSTRAINT [DF_PostType_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_PostType_DateUpdated]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[PostType] ADD  CONSTRAINT [DF_PostType_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_PostType_CreatedBy]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[PostType] ADD  CONSTRAINT [DF_PostType_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_PostType_UpdatedBy]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[PostType] ADD  CONSTRAINT [DF_PostType_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_Country_DateCreated]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Country_DateUpdated]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_Country_CreatedBy]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_Country_UpdatedBy]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_ContactType_DateCreated]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[ContactType] ADD  CONSTRAINT [DF_ContactType_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_ContactType_DateUpdated]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[ContactType] ADD  CONSTRAINT [DF_ContactType_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_ContactType_CreatedBy]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[ContactType] ADD  CONSTRAINT [DF_ContactType_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_ContactType_UpdatedBy]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[ContactType] ADD  CONSTRAINT [DF_ContactType_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_RoomType_DateCreated]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[RoomType] ADD  CONSTRAINT [DF_RoomType_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_RoomType_DateUpdated]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[RoomType] ADD  CONSTRAINT [DF_RoomType_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_RoomType_CreatedBy]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[RoomType] ADD  CONSTRAINT [DF_RoomType_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_RoomType_UpdatedBy]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[RoomType] ADD  CONSTRAINT [DF_RoomType_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_RealestateType_DateCreated]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[RealestateType] ADD  CONSTRAINT [DF_RealestateType_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_RealestateType_DateUpdated]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[RealestateType] ADD  CONSTRAINT [DF_RealestateType_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_RealestateType_CreatedBy]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[RealestateType] ADD  CONSTRAINT [DF_RealestateType_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_RealestateType_UpdatedBy]    Script Date: 10/02/2015 16:28:15 ******/
ALTER TABLE [dbo].[RealestateType] ADD  CONSTRAINT [DF_RealestateType_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_ContactInformation_FirstName]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_FirstName]  DEFAULT ('') FOR [FirstName]
GO
/****** Object:  Default [DF_ContactInformation_LastName]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_LastName]  DEFAULT ('') FOR [LastName]
GO
/****** Object:  Default [DF_Table_1_AddressLineOne]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_Table_1_AddressLineOne]  DEFAULT ('') FOR [Address]
GO
/****** Object:  Default [DF_ContactInformation_Address2]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_Address2]  DEFAULT ('') FOR [Address2]
GO
/****** Object:  Default [DF_Table_1_AddressLineTwo]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_Table_1_AddressLineTwo]  DEFAULT ('') FOR [City]
GO
/****** Object:  Default [DF_ContactInformation_State]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_State]  DEFAULT ('') FOR [State]
GO
/****** Object:  Default [DF_ContactInformation_Postcode]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_Postcode]  DEFAULT ('') FOR [Postcode]
GO
/****** Object:  Default [DF_ContactInformation_Country]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_Country]  DEFAULT ('') FOR [CountryId]
GO
/****** Object:  Default [DF_ContactInformation_PhoneNumber]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_PhoneNumber]  DEFAULT ('') FOR [PhoneNumber]
GO
/****** Object:  Default [DF_ContactInformation_FaxNumber]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_FaxNumber]  DEFAULT ('') FOR [FaxNumber]
GO
/****** Object:  Default [DF_ContactInformation_Email]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_Email]  DEFAULT ('') FOR [Email]
GO
/****** Object:  Default [DF_ContactInformation_DateCreated]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_ContactInformation_DateUpdated]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_ContactInformation_CreatedBy]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_ContactInformation_UpdatedBy]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_City_DateCreated]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[City] ADD  CONSTRAINT [DF_City_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_City_DateUpdated]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[City] ADD  CONSTRAINT [DF_City_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_City_CreatedBy]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[City] ADD  CONSTRAINT [DF_City_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_City_UpdatedBy]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[City] ADD  CONSTRAINT [DF_City_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_Post_IsLegacy]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_IsLegacy]  DEFAULT ((0)) FOR [IsLegacy]
GO
/****** Object:  Default [DF_Post_DateCreated]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Post_DateUpdated]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_Post_CreatedBy]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Post_UpdatedBy]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_Image_DateCreated]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[Image] ADD  CONSTRAINT [DF_Image_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Image_DateUpdated]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[Image] ADD  CONSTRAINT [DF_Image_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_Image_CreatedBy]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[Image] ADD  CONSTRAINT [DF_Image_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_Image_UpdatedBy]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[Image] ADD  CONSTRAINT [DF_Image_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_District_DateCreated]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[District] ADD  CONSTRAINT [DF_District_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_District_DateUpdated]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[District] ADD  CONSTRAINT [DF_District_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_District_CreatedBy]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[District] ADD  CONSTRAINT [DF_District_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_District_UpdatedBy]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[District] ADD  CONSTRAINT [DF_District_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  ForeignKey [FK_ContactInformation_ContactType]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation]  WITH CHECK ADD  CONSTRAINT [FK_ContactInformation_ContactType] FOREIGN KEY([ContactTypeId])
REFERENCES [dbo].[ContactType] ([ContactTypeId])
GO
ALTER TABLE [dbo].[ContactInformation] CHECK CONSTRAINT [FK_ContactInformation_ContactType]
GO
/****** Object:  ForeignKey [FK_ContactInformation_Country]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[ContactInformation]  WITH CHECK ADD  CONSTRAINT [FK_ContactInformation_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
ALTER TABLE [dbo].[ContactInformation] CHECK CONSTRAINT [FK_ContactInformation_Country]
GO
/****** Object:  ForeignKey [FK_City_Country]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_Country]
GO
/****** Object:  ForeignKey [FK_Post_PostType]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_PostType] FOREIGN KEY([PostTypeId])
REFERENCES [dbo].[PostType] ([PostTypeId])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_PostType]
GO
/****** Object:  ForeignKey [FK_Image_ImageType]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_ImageType] FOREIGN KEY([ImageTypeId])
REFERENCES [dbo].[ImageType] ([ImageTypeId])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_ImageType]
GO
/****** Object:  ForeignKey [FK_District_City]    Script Date: 10/02/2015 16:28:16 ******/
ALTER TABLE [dbo].[District]  WITH CHECK ADD  CONSTRAINT [FK_District_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([CityId])
GO
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_District_City]
GO
