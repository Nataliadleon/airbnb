namespace airbnb
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private Button btnLoad = null!;
        private TextBox txtSearch = null!;
        private CheckBox chkPetFriendly = null!;
        private Label lblSearch = null!;
        private Label lblStatus = null!;
        private DataGridView dgvData = null!;
        private OpenFileDialog openFileDialog = null!;
        
        // Controles adicionales de filtrado
        private ComboBox cmbPropertyType = null!;
        private Label lblPropertyType = null!;
        private NumericUpDown nudMinPrice = null!;
        private NumericUpDown nudMaxPrice = null!;
        private Label lblPriceRange = null!;
        private NumericUpDown nudMinBedrooms = null!;
        private NumericUpDown nudMaxBedrooms = null!;
        private Label lblBedrooms = null!;
        private NumericUpDown nudMinRating = null!;
        private Label lblMinRating = null!;
        private Button btnClearFilters = null!;
        private Label lblFilterInfo = null!;

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            
            // Panel superior para controles
            var panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 200,
                BackColor = SystemColors.Control,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Fila 1: Botón de carga y búsqueda
            btnLoad = new Button
            {
                Text = "📁 Cargar JSON",
                Location = new Point(10, 10),
                Size = new Size(100, 30),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnLoad.Click += BtnLoad_Click;
            panelTop.Controls.Add(btnLoad);

            lblSearch = new Label
            {
                Text = "🔍 Búsqueda:",
                Location = new Point(120, 10),
                AutoSize = true
            };
            panelTop.Controls.Add(lblSearch);

            txtSearch = new TextBox
            {
                Location = new Point(120, 30),
                Size = new Size(300, 25),
                PlaceholderText = "Buscar por nombre o descripción..."
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            panelTop.Controls.Add(txtSearch);

            chkPetFriendly = new CheckBox
            {
                Text = "🐾 Pet Friendly",
                Location = new Point(430, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            chkPetFriendly.CheckedChanged += ChkPetFriendly_CheckedChanged;
            panelTop.Controls.Add(chkPetFriendly);

            // Fila 2: Tipo de propiedad y rango de precio
            lblPropertyType = new Label
            {
                Text = "🏠 Tipo:",
                Location = new Point(10, 65),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            panelTop.Controls.Add(lblPropertyType);

            cmbPropertyType = new ComboBox
            {
                Location = new Point(10, 85),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPropertyType.Items.Add("(Todos los tipos)");
            cmbPropertyType.SelectedIndex = 0;
            cmbPropertyType.SelectedIndexChanged += CmbPropertyType_SelectedIndexChanged;
            panelTop.Controls.Add(cmbPropertyType);

            lblPriceRange = new Label
            {
                Text = "💰 Precio: Mín",
                Location = new Point(170, 65),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            panelTop.Controls.Add(lblPriceRange);

            nudMinPrice = new NumericUpDown
            {
                Location = new Point(170, 85),
                Size = new Size(80, 25),
                Minimum = 0,
                Maximum = 100000,
                Value = 0
            };
            nudMinPrice.ValueChanged += NudMinPrice_ValueChanged;
            panelTop.Controls.Add(nudMinPrice);

            var lblMaxPrice = new Label
            {
                Text = "Máx",
                Location = new Point(260, 65),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            panelTop.Controls.Add(lblMaxPrice);

            nudMaxPrice = new NumericUpDown
            {
                Location = new Point(260, 85),
                Size = new Size(80, 25),
                Minimum = 0,
                Maximum = 100000,
                Value = 100000
            };
            nudMaxPrice.ValueChanged += NudMaxPrice_ValueChanged;
            panelTop.Controls.Add(nudMaxPrice);

            // Fila 3: Dormitorios y Rating
            lblBedrooms = new Label
            {
                Text = "🛏️ Dormitorios: Mín",
                Location = new Point(350, 65),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            panelTop.Controls.Add(lblBedrooms);

            nudMinBedrooms = new NumericUpDown
            {
                Location = new Point(350, 85),
                Size = new Size(60, 25),
                Minimum = 0,
                Maximum = 20,
                Value = 0
            };
            nudMinBedrooms.ValueChanged += NudMinBedrooms_ValueChanged;
            panelTop.Controls.Add(nudMinBedrooms);

            var lblMaxBedrooms = new Label
            {
                Text = "Máx",
                Location = new Point(420, 65),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            panelTop.Controls.Add(lblMaxBedrooms);

            nudMaxBedrooms = new NumericUpDown
            {
                Location = new Point(420, 85),
                Size = new Size(60, 25),
                Minimum = 0,
                Maximum = 20,
                Value = 20
            };
            nudMaxBedrooms.ValueChanged += NudMaxBedrooms_ValueChanged;
            panelTop.Controls.Add(nudMaxBedrooms);

            lblMinRating = new Label
            {
                Text = "⭐ Rating mín:",
                Location = new Point(490, 65),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            panelTop.Controls.Add(lblMinRating);

            nudMinRating = new NumericUpDown
            {
                Location = new Point(490, 85),
                Size = new Size(70, 25),
                Minimum = 0,
                Maximum = 5,
                DecimalPlaces = 1,
                Increment = 0.1m,
                Value = 0
            };
            nudMinRating.ValueChanged += NudMinRating_ValueChanged;
            panelTop.Controls.Add(nudMinRating);

            btnClearFilters = new Button
            {
                Text = "🔄 Limpiar Filtros",
                Location = new Point(570, 30),
                Size = new Size(110, 30),
                BackColor = Color.LightGray,
                Font = new Font("Segoe UI", 9)
            };
            btnClearFilters.Click += BtnClearFilters_Click;
            panelTop.Controls.Add(btnClearFilters);

            // Label de estado y info de filtros
            lblStatus = new Label
            {
                Text = "Cargue un archivo JSON para comenzar",
                Location = new Point(10, 125),
                Size = new Size(600, 20),
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8)
            };
            panelTop.Controls.Add(lblStatus);

            lblFilterInfo = new Label
            {
                Text = "Filtros activos: ninguno",
                Location = new Point(10, 150),
                Size = new Size(600, 35),
                ForeColor = Color.DarkGreen,
                Font = new Font("Segoe UI", 8),
                AutoSize = false
            };
            panelTop.Controls.Add(lblFilterInfo);

            // DataGridView
            dgvData = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D
            };
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { Name = "ID", HeaderText = "ID", Width = 50 });

            // FileDialog - Configurable para cargar cualquier archivo JSON externo
            openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Seleccionar archivo JSON",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
                RestoreDirectory = true
            };

            // Configurar formulario
            Controls.Add(dgvData);
            Controls.Add(panelTop);
            
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 650);
            Text = "Gestor Dinámico de Datasets Airbnb - JSON (Filtrado Avanzado)";
            StartPosition = FormStartPosition.CenterScreen;
            Icon = null;
        }

        #endregion
    }
}
