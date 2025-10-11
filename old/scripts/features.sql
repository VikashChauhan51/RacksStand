-- Enable the uuid-ossp extension for generating UUIDs
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Insert data into the "Modules" table
INSERT INTO public."Modules" ("Id", "CreatedAt", "IsActive", "ConcurrencyStamp", "Name", "Description")
VALUES
    ('00000000-0000-0000-0000-000000000001', NOW(), TRUE, uuid_generate_v4(), 'User Management', 'Handles user-related operations'),
    ('00000000-0000-0000-0000-000000000002', NOW(), TRUE, uuid_generate_v4(), 'Billing', 'Manages billing and subscriptions');

-- Insert data into the "Features" table
INSERT INTO public."Features" ("Id", "CreatedAt",  "IsActive", "ConcurrencyStamp", "Name", "Code", "Description")
VALUES
    ('00000000-0000-0000-0000-000000000301', NOW(), TRUE, uuid_generate_v4(), 'Basic Feature', 'FEATURE_BASIC', 'Feature set for basic plans'),
    ('00000000-0000-0000-0000-000000000302', NOW(), TRUE, uuid_generate_v4(), 'Pro Feature', 'FEATURE_PRO', 'Feature set for pro plans'),
    ('00000000-0000-0000-0000-000000000303', NOW(), TRUE, uuid_generate_v4(), 'Enterprise Feature', 'FEATURE_ENTERPRISE', 'Feature set for enterprise plans');

-- Insert data into the "ModulePermissions" table
INSERT INTO public."Permissions" ("Id", "CreatedAt", "IsActive", "ConcurrencyStamp", "ModuleId", "Name", "Code", "Description")
VALUES
    ('00000000-0000-0000-0000-000000000011', NOW(), TRUE, uuid_generate_v4(), '00000000-0000-0000-0000-000000000001', 'View Users', 'USER_VIEW', 'Permission to view users'),
    ('00000000-0000-0000-0000-000000000012', NOW(), TRUE, uuid_generate_v4(), '00000000-0000-0000-0000-000000000001', 'Edit Users', 'USER_EDIT', 'Permission to edit users'),
    ('00000000-0000-0000-0000-000000000013', NOW(), TRUE, uuid_generate_v4(), '00000000-0000-0000-0000-000000000002', 'View Invoices', 'INVOICE_VIEW', 'Permission to view invoices');

-- Insert data into the "Plans" table
INSERT INTO public."Plans" ("Id", "CreatedAt", "IsActive", "ConcurrencyStamp", "Name", "Price", "DurationInMonths", "SupportLevel", "Description")
VALUES
    ('00000000-0000-0000-0000-000000000101', NOW(), TRUE, uuid_generate_v4(), 'Basic Plan', 9.99, 1, 'Email', 'Basic subscription with minimal features'),
    ('00000000-0000-0000-0000-000000000102', NOW(), TRUE, uuid_generate_v4(), 'Pro Plan', 29.99, 12, 'Email and Phone', 'Pro subscription with additional features'),
    ('00000000-0000-0000-0000-000000000103', NOW(), TRUE, uuid_generate_v4(), 'Enterprise Plan', 99.99, 12, 'Priority Support', 'Enterprise-level subscription with all features');

-- Insert data into the "PlanFeatures" table
INSERT INTO public."PlanFeatures" ("Id", "CreatedAt", "IsActive", "ConcurrencyStamp", "PlanId", "FeatureId", "Value")
VALUES
    ('00000000-0000-0000-0000-000000000201', NOW(), TRUE, uuid_generate_v4(), '00000000-0000-0000-0000-000000000101', '00000000-0000-0000-0000-000000000301', 'Basic feature set'),
    ('00000000-0000-0000-0000-000000000202', NOW(), TRUE, uuid_generate_v4(), '00000000-0000-0000-0000-000000000102', '00000000-0000-0000-0000-000000000302', 'Advanced feature set'),
    ('00000000-0000-0000-0000-000000000203', NOW(), TRUE, uuid_generate_v4(), '00000000-0000-0000-0000-000000000103', '00000000-0000-0000-0000-000000000303', 'Complete feature set');
