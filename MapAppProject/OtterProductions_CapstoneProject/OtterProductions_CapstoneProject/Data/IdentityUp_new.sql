IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [FirstName] nvarchar(50) NULL,
    [LastName] nvarchar(50) NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [IsOrganization] bit NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230227052057_init_authdbcontext', N'7.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [EventType] (
    [ID] int NOT NULL IDENTITY,
    [EventType] nvarchar(255) NULL,
    CONSTRAINT [PK__EventTyp__3214EC27C715A1DA] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [MapAppUser] (
    [ID] int NOT NULL IDENTITY,
    [AspnetIdentityId] nvarchar(50) NULL,
    CONSTRAINT [PK__MapAppUs__3214EC27085551E5] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Organization] (
    [ID] int NOT NULL IDENTITY,
    [AspnetIdentityId] nvarchar(50) NULL,
    [Email] nvarchar(50) NOT NULL,
    [Address] nvarchar(50) NULL,
    [OrganizationName] nvarchar(50) NOT NULL,
    [OrganizationDescription] nvarchar(50) NULL,
    [OrganizationLocation] nvarchar(50) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    CONSTRAINT [PK__Organzat__3214EC27E4991DB4] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Event] (
    [ID] int NOT NULL IDENTITY,
    [OrganizationID] int NOT NULL,
    [EventName] nvarchar(255) NOT NULL,
    [EventLocation] nvarchar(255) NOT NULL,
    [EventTypeID] int NOT NULL,
    [EventDescription] nvarchar(255) NOT NULL,
    CONSTRAINT [PK__Event__3214EC2726B95BDA] PRIMARY KEY ([ID]),
    CONSTRAINT [Fk EventType ID] FOREIGN KEY ([OrganizationID]) REFERENCES [EventType] ([ID]),
    CONSTRAINT [Fk Organization ID] FOREIGN KEY ([OrganizationID]) REFERENCES [Organization] ([ID])
);
GO

CREATE TABLE [UserEventList] (
    [ID] int NOT NULL IDENTITY,
    [MapAppUserID] int NOT NULL,
    [EventID] int NOT NULL,
    CONSTRAINT [PK__UserEven__3214EC27DEE07ADF] PRIMARY KEY ([ID]),
    CONSTRAINT [Fk Event ID] FOREIGN KEY ([EventID]) REFERENCES [Event] ([ID]),
    CONSTRAINT [Fk MapAppUser ID] FOREIGN KEY ([MapAppUserID]) REFERENCES [MapAppUser] ([ID])
);
GO

CREATE INDEX [IX_Event_OrganizationID] ON [Event] ([OrganizationID]);
GO

CREATE INDEX [IX_UserEventList_EventID] ON [UserEventList] ([EventID]);
GO

CREATE INDEX [IX_UserEventList_MapAppUserID] ON [UserEventList] ([MapAppUserID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230227052326_init_mapappdbcontext', N'7.0.3');
GO

COMMIT;
GO

