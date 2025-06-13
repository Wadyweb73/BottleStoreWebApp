-- ========================================
-- 1. TABELA users
-- ========================================
CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    phone_number NVARCHAR(20),
    email_address NVARCHAR(100) NOT NULL UNIQUE,
    password NVARCHAR(255) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    user_type TINYINT NOT NULL,
    is_approved BIT DEFAULT 0,
    is_active BIT DEFAULT 1
);

-- ========================================
-- 2. TABELA products
-- ========================================
CREATE TABLE products (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_name NVARCHAR(200) NOT NULL,
    product_category NVARCHAR(100) NOT NULL,
    product_size NVARCHAR(50),
    created_at DATETIME DEFAULT GETDATE(),
    is_active BIT DEFAULT 1
);

-- ========================================
-- 3. TABELA product_information
-- ========================================
CREATE TABLE product_information (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    amount_boxes INT NOT NULL DEFAULT 0,
    purchase_price DECIMAL(10,2) NOT NULL,
    unit_price DECIMAL(10,2) NOT NULL,
    register_date DATETIME DEFAULT GETDATE(),
    product_supplier NVARCHAR(200),
    is_active BIT DEFAULT 1,

    FOREIGN KEY (product_id) REFERENCES products(id)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- ========================================
-- 4. TABELA sales
-- ========================================
CREATE TABLE sales (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    user_id INT NOT NULL,
    amount INT NOT NULL,
    unit_price DECIMAL(10,2) NOT NULL,
    total_amount DECIMAL(10,2) NOT NULL,
    sale_date DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (product_id) REFERENCES products(id)
        ON DELETE NO ACTION ON UPDATE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id)
        ON DELETE NO ACTION ON UPDATE CASCADE
);

-- ========================================
-- 5. TABELA sale_receipts
-- ========================================
CREATE TABLE sale_receipts (
    id INT IDENTITY(1,1) PRIMARY KEY,
    sale_id INT NOT NULL,
    sale_date DATETIME NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    customer_name NVARCHAR(200),
    customer_phone NVARCHAR(20),
    payment_method NVARCHAR(50),

    FOREIGN KEY (sale_id) REFERENCES sales(id)
    ON DELETE CASCADE ON UPDATE CASCADE
);

-- ========================================
-- 6. TABELA user_approval_requests
-- ========================================
CREATE TABLE user_approval_requests (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    request_date DATETIME DEFAULT GETDATE(),
    approved_by INT NULL,
    approval_date DATETIME NULL,
    status TINYINT DEFAULT 0,
    comments NVARCHAR(MAX),

    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (approved_by) REFERENCES users(id)
);

-- ========================================
-- 7. TABELA inventory_logs
-- ========================================
CREATE TABLE inventory_logs (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT,
    user_id INT,
    movement_type TINYINT NOT NULL,
    quantity INT NOT NULL,
    previous_stock INT NOT NULL,
    new_stock INT NOT NULL,
    reason NVARCHAR(MAX),
    movement_date DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (product_id) REFERENCES products(id)
        ON DELETE NO ACTION ON UPDATE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id)
        ON DELETE NO ACTION ON UPDATE CASCADE
);

