# RacksStand
Rack-Based Inventory Management System.

## Introduction

This document outlines the requirements and assumptions for a rack-based inventory management system designed to help shopkeepers efficiently manage their stock within racks containing pockets or boxes. The system aims to address the challenges of manually locating products, reducing search time, and preventing lost sales due to misplaced items.

## Goals

*   Improve inventory accuracy and reduce discrepancies.
*   Minimize product search time for shopkeepers.
*   Prevent lost sales due to misplaced or unlocatable items.
*   Provide a user-friendly interface for managing racks, pockets, and products.
*   Offer reporting capabilities for inventory analysis.

## Scope

This project will focus on the following features:

*   Management of shops, racks,categories, products, and users.
*   Management of racks, rows within racks, and pockets within rows.
*   Product cataloging with detailed product information.
*   Assignment of products to specific pockets.
*   Inventory tracking and updates.
*   Product lookup and location retrieval.
*   Basic reporting on inventory levels.

## Functional Requirements

### Shop Management
*   Users can manage multiple shops under the same account.
*   Each shop can be activated or deactivated.
*   A user must be associated with at least one shop to use the system.
*   Only super users have the ability to manage shops (add, edit, delete, activate, deactivate).
*   Each shop has its own categories, products, racks, and associated data.

### User Management
*   Users can only access data related to the shops they are associated with.
*   The system enforces that a user cannot exist without being linked to at least one shop.
*   User roles include super users (with shop management permissions) and standard users (limited to inventory management).

### Rack Management
*   Ability to define racks with a specified number of rows.
*   Ability to define the number of pockets within each row (allowing for variations between rows).
*   Ability to name and describe racks for easy identification.
*   Ability to assign unique barcodes to racks (optional).

### Pocket Management
*   Ability to define the capacity of each pocket (number of units).
*   Ability to assign unique barcodes to pockets.
*   Ability for shopkeepers to manually set the state of a pocket (empty, partially full, full).

### Product Cataloging
*   Ability to add new products with details like name, description, category, supplier (optional), barcode, purchase price, selling price, and unit.
*   Ability to categorize products under shop-specific categories.
*   Ability to manage product information (edit, delete).

### Inventory Management
*   Ability to assign products to specific pockets with a specified quantity.
*   Ability to update inventory levels (add/remove products from pockets).
*   System to check if adding products to a pocket exceeds its capacity.

### Product Lookup
*   Ability to search for products by name, barcode, or category.
*   Ability to view the location of a product (rack, row, and pocket).

### Reporting
*   Generate reports on current stock levels for each product.
*   Generate low stock alerts.

## Non-Functional Requirements

*   **Usability:** The system should be intuitive and easy to use for shopkeepers with varying levels of technical expertise.
*   **Performance:** The system should be responsive and provide quick search results.
*   **Security:** The system should protect sensitive data (e.g., product pricing, user credentials).
*   **Scalability:** The system should be able to handle a growing inventory and number of users.
*   **Reliability:** The system should be reliable and minimize data loss.

## Assumptions

*   Shopkeepers have access to devices with barcode scanners (e.g., smartphones, dedicated barcode scanners).
*   Shopkeepers are responsible for accurately setting the pocket state (empty, partially full, full).
*   The system will not handle complex logistics like warehouse management or shipping.
*   The system will not integrate with external accounting or POS systems in the initial phase (MVP).
*   The system will be developed using .NET technologies and PostgreSQL as the database.
*   The project will follow Clean Architecture principles.

## Out of Scope

*   Integration with external systems (e.g., accounting, POS).
*   Advanced analytics or forecasting.
*   Automated pocket state management based on product dimensions.
*   Warehouse management features.
*   Shipping and delivery management.

## Future Considerations

*   Integration with e-commerce platforms.
*   Advanced reporting and analytics.
*   Mobile application for remote inventory management.
*   Automated pocket state management.

## Glossary

*   **Rack:** A physical storage unit with multiple rows and columns for holding items.
*   **Row:** A horizontal section within a rack.
*   **Pocket:** A specific storage location within a row (also referred to as a box or compartment).
*   **SKU:** Stock Keeping Unit (a unique identifier for a product).

