-- ========================================
-- BOTTLE STORE DATABASE CREATION SCRIPT
-- MySQL/MariaDB Version with snake_case
-- ========================================

-- Criar o banco de dados (se necessário)
-- CREATE DATABASE bottle_store_db;
-- USE bottle_store_db;

-- ========================================
-- 1. TABELA users
-- ========================================
CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    phone_number VARCHAR(20),
    email_address VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL, -- Hash da senha
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    user_type TINYINT NOT NULL, -- 0=Admin, 1=Funcionario, 2=Cliente
    is_approved BOOLEAN DEFAULT FALSE, -- Para controle de aprovação
    is_active BOOLEAN DEFAULT TRUE
);

-- ========================================
-- 2. TABELA products
-- ========================================
CREATE TABLE products (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_name VARCHAR(200) NOT NULL,
    product_category VARCHAR(100) NOT NULL,
    product_size VARCHAR(50), -- Ex: 350ml, 500ml, 1L
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE
);

-- ========================================
-- 3. TABELA product_information
-- ========================================
CREATE TABLE product_information (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    amount_boxes INT NOT NULL DEFAULT 0, -- Quantidade em estoque
    purchase_price DECIMAL(10,2) NOT NULL, -- Preço de compra
    unit_price DECIMAL(10,2) NOT NULL, -- Preço de venda
    register_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    product_supplier VARCHAR(200),
    is_active BOOLEAN DEFAULT TRUE,
    
    -- Chave estrangeira
    CONSTRAINT fk_product_info_product FOREIGN KEY (product_id) REFERENCES products(id)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- ========================================
-- 4. TABELA sales
-- ========================================
CREATE TABLE sales (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    user_id INT NOT NULL, -- Funcionário que fez a venda
    amount INT NOT NULL, -- Quantidade vendida
    unit_price DECIMAL(10,2) NOT NULL, -- Preço unitário na venda
    total_amount DECIMAL(10,2) NOT NULL, -- Total da venda
    sale_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    -- Chaves estrangeiras
    CONSTRAINT fk_sales_product FOREIGN KEY (product_id) REFERENCES products(id)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT fk_sales_user FOREIGN KEY (user_id) REFERENCES users(id)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

-- ========================================
-- 5. TABELA sale_receipts
-- ========================================
CREATE TABLE sale_receipts (
    id INT AUTO_INCREMENT PRIMARY KEY,
    sale_id INT NOT NULL,
    sale_date TIMESTAMP NOT NULL,
    amount DECIMAL(10,2) NOT NULL, -- Total do recibo
    -- Campos adicionais para o recibo
    customer_name VARCHAR(200),
    customer_phone VARCHAR(20),
    payment_method VARCHAR(50), -- Dinheiro, Cartão, etc.
    
    -- Chave estrangeira
    CONSTRAINT fk_sale_receipts_sale FOREIGN KEY (sale_id) REFERENCES sales(id)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- ========================================
-- 6. TABELA ADICIONAL: user_approval_requests
-- Para gerenciar solicitações de aprovação
-- ========================================
CREATE TABLE user_approval_requests (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    request_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    approved_by INT NULL, -- ID do admin que aprovou
    approval_date TIMESTAMP NULL,
    status TINYINT DEFAULT 0, -- 0=Pendente, 1=Aprovado, 2=Rejeitado
    comments TEXT,
    
    -- Chaves estrangeiras
    CONSTRAINT fk_approval_req_user FOREIGN KEY (user_id) REFERENCES users(id)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_approval_req_admin FOREIGN KEY (approved_by) REFERENCES users(id)
        ON DELETE SET NULL ON UPDATE CASCADE
);

-- ========================================
-- 7. TABELA ADICIONAL: inventory_logs
-- Para controle de estoque
-- ========================================
CREATE TABLE inventory_logs (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    user_id INT NOT NULL, -- Quem fez a alteração
    movement_type TINYINT NOT NULL, -- 1=Entrada, 2=Saída, 3=Ajuste
    quantity INT NOT NULL,
    previous_stock INT NOT NULL,
    new_stock INT NOT NULL,
    reason TEXT,
    movement_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    -- Chaves estrangeiras
    CONSTRAINT fk_inventory_log_product FOREIGN KEY (product_id) REFERENCES products(id)
        ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_inventory_log_user FOREIGN KEY (user_id) REFERENCES users(id)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

-- ========================================
-- ÍNDICES PARA MELHOR PERFORMANCE
-- ========================================
CREATE INDEX idx_users_email ON users(email_address);
CREATE INDEX idx_users_user_type ON users(user_type);
CREATE INDEX idx_users_is_approved ON users(is_approved);
CREATE INDEX idx_products_category ON products(product_category);
CREATE INDEX idx_products_is_active ON products(is_active);
CREATE INDEX idx_sales_date ON sales(sale_date);
CREATE INDEX idx_sales_product ON sales(product_id);
CREATE INDEX idx_sales_user ON sales(user_id);
CREATE INDEX idx_product_info_product ON product_information(product_id);
CREATE INDEX idx_approval_status ON user_approval_requests(status);

-- ========================================
-- DADOS INICIAIS
-- ========================================

-- Inserir usuário administrador padrão
INSERT INTO users (name, email_address, password, user_type, is_approved, is_active) 
VALUES ('Administrador', 'admin@bottlestore.com', '$2a$11$HASH_DA_SENHA_AQUI', 0, TRUE, TRUE);

-- Categorias de produtos comuns para bottle store
INSERT INTO products (product_name, product_category, product_size) VALUES
('Cerveja Skol', 'Cerveja', '350ml'),
('Cerveja Heineken', 'Cerveja', '330ml'),
('Vodka Smirnoff', 'Destilados', '750ml'),
('Vinho Tinto', 'Vinhos', '750ml'),
('Whisky Black Label', 'Destilados', '1L'),
('Água Mineral', 'Não Alcoólico', '500ml'),
('Refrigerante Coca-Cola', 'Não Alcoólico', '350ml'),
('Cerveja Sagres', 'Cerveja', '330ml'),
('Gin Bombay', 'Destilados', '750ml'),
('Vinho Branco', 'Vinhos', '750ml');

-- Inserir informações de produto (exemplo)
INSERT INTO product_information (product_id, amount_boxes, purchase_price, unit_price, product_supplier) VALUES
(1, 50, 25.00, 35.00, 'Distribuidora Central'),
(2, 30, 45.00, 65.00, 'Importadora Premium'),
(3, 20, 180.00, 250.00, 'Bebidas Finas Lda'),
(4, 25, 120.00, 180.00, 'Vinhos & Cia'),
(5, 15, 450.00, 650.00, 'Destilados Premium');

-- ========================================
-- VIEWS ÚTEIS
-- ========================================

-- View para produtos com informações completas
CREATE VIEW vw_products_full AS
SELECT 
    p.id,
    p.product_name,
    p.product_category,
    p.product_size,
    pi.amount_boxes,
    pi.purchase_price,
    pi.unit_price,
    pi.product_supplier,
    p.created_at,
    p.is_active
FROM products p
LEFT JOIN product_information pi ON p.id = pi.product_id
WHERE p.is_active = TRUE;

-- View para vendas com detalhes
CREATE VIEW vw_sales_detailed AS
SELECT 
    s.id,
    s.sale_date,
    s.amount,
    s.unit_price,
    s.total_amount,
    p.product_name,
    p.product_category,
    u.name as seller_name
FROM sales s
JOIN products p ON s.product_id = p.id
JOIN users u ON s.user_id = u.id;

-- View para usuários pendentes de aprovação
CREATE VIEW vw_pending_users AS
SELECT 
    u.id,
    u.name,
    u.email_address,
    u.phone_number,
    u.created_at,
    uar.request_date,
    uar.comments
FROM users u
JOIN user_approval_requests uar ON u.id = uar.user_id
WHERE u.is_approved = FALSE AND uar.status = 0;

-- ========================================
-- PROCEDURES ÚTEIS
-- ========================================

-- Procedure para aprovar usuário
DELIMITER //
CREATE PROCEDURE sp_approve_user(
    IN p_user_id INT,
    IN p_admin_id INT,
    IN p_comments TEXT
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;
    
    START TRANSACTION;
    
    -- Atualizar usuário
    UPDATE users 
    SET is_approved = TRUE 
    WHERE id = p_user_id;
    
    -- Atualizar solicitação de aprovação
    UPDATE user_approval_requests 
    SET status = 1, 
        approved_by = p_admin_id, 
        approval_date = CURRENT_TIMESTAMP,
        comments = p_comments
    WHERE user_id = p_user_id AND status = 0;
    
    COMMIT;
END //
DELIMITER ;

-- Procedure para registrar venda e atualizar estoque
DELIMITER //
CREATE PROCEDURE sp_register_sale(
    IN p_product_id INT,
    IN p_user_id INT,
    IN p_amount INT,
    IN p_unit_price DECIMAL(10,2)
)
BEGIN
    DECLARE v_current_stock INT;
    DECLARE v_total_amount DECIMAL(10,2);
    DECLARE v_sale_id INT;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;
    
    START TRANSACTION;
    
    -- Verificar estoque
    SELECT amount_boxes INTO v_current_stock
    FROM product_information 
    WHERE product_id = p_product_id;
    
    IF v_current_stock < p_amount THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Estoque insuficiente';
    END IF;
    
    -- Calcular total
    SET v_total_amount = p_amount * p_unit_price;
    
    -- Inserir venda
    INSERT INTO sales (product_id, user_id, amount, unit_price, total_amount)
    VALUES (p_product_id, p_user_id, p_amount, p_unit_price, v_total_amount);
    
    SET v_sale_id = LAST_INSERT_ID();
    
    -- Atualizar estoque
    UPDATE product_information 
    SET amount_boxes = amount_boxes - p_amount
    WHERE product_id = p_product_id;
    
    -- Log do movimento
    INSERT INTO inventory_logs (product_id, user_id, movement_type, quantity, previous_stock, new_stock, reason)
    VALUES (p_product_id, p_user_id, 2, p_amount, v_current_stock, v_current_stock - p_amount, 'Venda');
    
    COMMIT;
    
    SELECT v_sale_id as sale_id;
END //
DELIMITER ;

-- ========================================
-- ENUMS PARA REFERÊNCIA
-- ========================================
/*
user_type:
0 = Administrador
1 = Funcionário  
2 = Cliente

status (approval):
0 = Pendente
1 = Aprovado
2 = Rejeitado

movement_type:
1 = Entrada (compra/recebimento)
2 = Saída (venda)
3 = Ajuste (correção de estoque)
*/
