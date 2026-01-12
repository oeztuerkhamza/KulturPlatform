-- Migration: UpdateActivitySchema
-- This script updates the Activities table schema to match the new domain model

BEGIN TRANSACTION;

-- Step 1: Drop old primary key from ActivityGalleryImages
ALTER TABLE [ActivityGalleryImages] DROP CONSTRAINT [PK_ActivityGalleryImages];

-- Step 2: Drop old Date columns
ALTER TABLE [Activities] DROP COLUMN [DateDe_DateISO];
ALTER TABLE [Activities] DROP COLUMN [DateDe_TextDe];
ALTER TABLE [Activities] DROP COLUMN [DateDe_TextTr];
ALTER TABLE [Activities] DROP COLUMN [DateTr_TextDe];
ALTER TABLE [Activities] DROP COLUMN [DateTr_TextTr];

-- Step 3: Rename Location columns to Address
EXEC sp_rename 'Activities.Location_ZipCode', 'Address_ZipCode', 'COLUMN';
EXEC sp_rename 'Activities.Location_State', 'Address_State', 'COLUMN';
EXEC sp_rename 'Activities.Location_Country', 'Address_Country', 'COLUMN';
EXEC sp_rename 'Activities.Location_City', 'Address_City', 'COLUMN';
EXEC sp_rename 'Activities.Location_Address', 'Address_Street', 'COLUMN';
EXEC sp_rename 'Activities.DateTr_DateISO', 'DateIso', 'COLUMN';

-- Step 4: Update Address_HouseNo column type
ALTER TABLE [Activities] ALTER COLUMN [Address_HouseNo] nvarchar(50) NOT NULL;

-- Step 5: Add new primary key to ActivityGalleryImages
ALTER TABLE [ActivityGalleryImages] ADD CONSTRAINT [PK_ActivityGalleryImages] PRIMARY KEY ([Id]);

-- Step 6: Create index on ActivityId
CREATE INDEX [IX_ActivityGalleryImages_ActivityId] ON [ActivityGalleryImages] ([ActivityId]);

-- Step 7: Insert migration history record
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260112105824_UpdateActivitySchema', N'10.0.1');

COMMIT TRANSACTION;
