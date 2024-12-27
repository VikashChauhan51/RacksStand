-- Insert Plans
INSERT INTO public."Plans"(
    "Id", "Name", "Price", "Description", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy", "IsActive")
VALUES
    ('free', 'Free Plan', 0, 'A basic plan for individuals with limited needs.', NOW(), 'System', NULL, NULL, TRUE),
    ('starter', 'Starter Plan', 9.99, 'An affordable plan for small businesses.', NOW(), 'System', NULL, NULL, TRUE),
    ('economy', 'Economy Plan', 29.99, 'A mid-tier plan for growing businesses.', NOW(), 'System', NULL, NULL, TRUE),
    ('premium', 'Premium Plan', 49.99, 'A top-tier plan for large businesses with premium support.', NOW(), 'System', NULL, NULL, TRUE);

-- Insert Features
INSERT INTO public."Features"(
    "Id", "Name", "Code", "Description", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy", "IsActive")
VALUES
    ('max_shops', 'Max Shops', 'MAX_SHOPS', 'Maximum number of shops allowed', NOW(), 'System', NULL, NULL, TRUE),
    ('max_categories', 'Max Categories', 'MAX_CATEGORIES', 'Maximum number of categories allowed', NOW(), 'System', NULL, NULL, TRUE),
    ('max_products', 'Max Products', 'MAX_PRODUCTS', 'Maximum number of products allowed', NOW(), 'System', NULL, NULL, TRUE),
    ('max_ranks', 'Max Ranks', 'MAX_RANKS', 'Maximum number of ranks allowed', NOW(), 'System', NULL, NULL, TRUE),
    ('max_rows', 'Max Rows', 'MAX_ROWS', 'Maximum number of rows allowed per rank', NOW(), 'System', NULL, NULL, TRUE),
    ('max_pockets', 'Max Pockets', 'MAX_POCKETS', 'Maximum number of pockets allowed per row', NOW(), 'System', NULL, NULL, TRUE),
    ('support_level', 'Support Level', 'SUPPORT_LEVEL', 'Level of customer support provided', NOW(), 'System', NULL, NULL, TRUE);

-- Insert PlanFeatures
INSERT INTO public."PlanFeatures"(
    "Id", "PlanId", "FeatureId", "Value", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy", "IsActive")
VALUES
    -- Free Plan Features
    ('free_max_shops', 'free', 'max_shops', '1', NOW(), 'System', NULL, NULL, TRUE),
    ('free_max_categories', 'free', 'max_categories', '10', NOW(), 'System', NULL, NULL, TRUE),
    ('free_max_products', 'free', 'max_products', '20', NOW(), 'System', NULL, NULL, TRUE),
    ('free_max_ranks', 'free', 'max_ranks', '2', NOW(), 'System', NULL, NULL, TRUE),
    ('free_max_rows', 'free', 'max_rows', '5', NOW(), 'System', NULL, NULL, TRUE),
    ('free_max_pockets', 'free', 'max_pockets', '10', NOW(), 'System', NULL, NULL, TRUE),

    -- Starter Plan Features
    ('starter_max_shops', 'starter', 'max_shops', '5', NOW(), 'System', NULL, NULL, TRUE),
    ('starter_max_categories', 'starter', 'max_categories', '50', NOW(), 'System', NULL, NULL, TRUE),
    ('starter_max_products', 'starter', 'max_products', '100', NOW(), 'System', NULL, NULL, TRUE),
    ('starter_max_ranks', 'starter', 'max_ranks', '10', NOW(), 'System', NULL, NULL, TRUE),
    ('starter_max_rows', 'starter', 'max_rows', '15', NOW(), 'System', NULL, NULL, TRUE),
    ('starter_max_pockets', 'starter', 'max_pockets', '20', NOW(), 'System', NULL, NULL, TRUE),

    -- Economy Plan Features
    ('economy_max_shops', 'economy', 'max_shops', '50', NOW(), 'System', NULL, NULL, TRUE),
    ('economy_max_categories', 'economy', 'max_categories', '150', NOW(), 'System', NULL, NULL, TRUE),
    ('economy_max_products', 'economy', 'max_products', '500', NOW(), 'System', NULL, NULL, TRUE),
    ('economy_max_ranks', 'economy', 'max_ranks', '100', NOW(), 'System', NULL, NULL, TRUE),
    ('economy_max_rows', 'economy', 'max_rows', '50', NOW(), 'System', NULL, NULL, TRUE),
    ('economy_max_pockets', 'economy', 'max_pockets', '100', NOW(), 'System', NULL, NULL, TRUE),
    ('economy_support_level', 'economy', 'support_level', '1-5 Business Days', NOW(), 'System', NULL, NULL, TRUE),

    -- Premium Plan Features
    ('premium_max_shops', 'premium', 'max_shops', '100', NOW(), 'System', NULL, NULL, TRUE),
    ('premium_max_categories', 'premium', 'max_categories', '150', NOW(), 'System', NULL, NULL, TRUE),
    ('premium_max_products', 'premium', 'max_products', '500', NOW(), 'System', NULL, NULL, TRUE),
    ('premium_max_ranks', 'premium', 'max_ranks', '500', NOW(), 'System', NULL, NULL, TRUE),
    ('premium_max_rows', 'premium', 'max_rows', '50', NOW(), 'System', NULL, NULL, TRUE),
    ('premium_max_pockets', 'premium', 'max_pockets', '100', NOW(), 'System', NULL, NULL, TRUE),
    ('premium_support_level', 'premium', 'support_level', '24/7 Support', NOW(), 'System', NULL, NULL, TRUE);
