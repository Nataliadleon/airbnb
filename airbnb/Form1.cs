namespace airbnb;

/// <summary>
/// Formulario principal para gestionar datasets de Airbnb en JSON.
/// Implementa carga dinámica, visualización adaptativa y filtrado avanzado con múltiples criterios.
/// </summary>
public partial class Form1 : Form
{
    private readonly JsonDataProcessor _processor = new();
    private bool _isInitialized = false;

    public Form1()
    {
        InitializeComponent();
        _isInitialized = true;
    }

    /// <summary>
    /// Evento Click del botón Cargar JSON.
    /// Abre diálogo y carga el archivo JSON externo.
    /// Soporta cualquier estructura JSON (arrays u objetos).
    /// </summary>
    private void BtnLoad_Click(object? sender, EventArgs e)
    {
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                var filePath = openFileDialog.FileName;
                var fileName = Path.GetFileName(filePath);

                // Verificar que el archivo existe
                if (!File.Exists(filePath))
                {
                    MessageBox.Show(
                        $"Error: El archivo '{fileName}' no existe.",
                        "Archivo no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // Verificar tamaño del archivo (máximo 50 MB)
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 50 * 1024 * 1024)
                {
                    MessageBox.Show(
                        "Error: El archivo es demasiado grande (máximo 50 MB).",
                        "Archivo demasiado grande",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                _processor.Clear();
                dgvData.Columns.Clear();

                if (!_processor.LoadFromJson(filePath))
                {
                    MessageBox.Show(
                        $"Error: No se pudo cargar el archivo JSON '{fileName}'.\n\n" +
                        "Verifique que:\n" +
                        "• El archivo contiene JSON válido\n" +
                        "• Es un array u objeto JSON\n" +
                        "• No está corrupto o codificado de forma diferente",
                        "Error de carga",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // Generar columnas dinámicamente basadas en el JSON
                PopulateDynamicColumns();

                // Poblar ComboBox de tipos de propiedad
                PopulatePropertyTypeCombo();

                // Mostrar datos
                RefreshDataGridView();

                // Actualizar estado
                UpdateStatus();

                // Mostrar mensaje de éxito
                var recordCount = _processor.TotalRecordCount;
                MessageBox.Show(
                    $"✓ Archivo '{fileName}' cargado exitosamente.\n\n" +
                    $"Registros cargados: {recordCount}\n" +
                    $"Columnas detectadas: {_processor.AvailableColumns.Count()}",
                    "Carga exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Resetear filtros
                _isInitialized = false;
                txtSearch.Clear();
                chkPetFriendly.Checked = false;
                cmbPropertyType.SelectedIndex = 0;
                nudMinPrice.Value = 0;
                nudMaxPrice.Value = 100000;
                nudMinBedrooms.Value = 0;
                nudMaxBedrooms.Value = 20;
                nudMinRating.Value = 0;
                _isInitialized = true;
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show(
                    "Error: No hay memoria suficiente para cargar el archivo.\n" +
                    "Intente con un archivo más pequeño.",
                    "Error de memoria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(
                    "Error: No tiene permisos para acceder al archivo.\n" +
                    "Verifique los permisos de acceso.",
                    "Error de acceso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error inesperado: {ex.Message}\n\n" +
                    $"Tipo: {ex.GetType().Name}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }

    /// <summary>
    /// Genera columnas dinámicamente basadas en las propiedades del JSON.
    /// </summary>
    private void PopulateDynamicColumns()
    {
        dgvData.Columns.Clear();

        // Añadir columna de índice
        dgvData.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Index",
            HeaderText = "#",
            Width = 40,
            ReadOnly = true
        });

        // Añadir columnas para cada propiedad encontrada
        foreach (var column in _processor.AvailableColumns.OrderBy(x => x))
        {
            var textColumn = new DataGridViewTextBoxColumn
            {
                Name = column,
                HeaderText = FormatColumnName(column),
                Width = 150,
                ReadOnly = true,
                Resizable = DataGridViewTriState.True
            };
            dgvData.Columns.Add(textColumn);
        }
    }

    /// <summary>
    /// Puebla el ComboBox con los tipos de propiedad disponibles.
    /// </summary>
    private void PopulatePropertyTypeCombo()
    {
        _isInitialized = false;

        cmbPropertyType.Items.Clear();
        cmbPropertyType.Items.Add("(Todos los tipos)");

        foreach (var propType in _processor.AvailablePropertyTypes)
        {
            if (!string.IsNullOrWhiteSpace(propType))
            {
                cmbPropertyType.Items.Add(propType);
            }
        }

        cmbPropertyType.SelectedIndex = 0;
        _isInitialized = true;
    }

    /// <summary>
    /// Formatea el nombre de la columna para que sea más legible.
    /// Convierte "property_type" a "Property Type".
    /// </summary>
    private string FormatColumnName(string columnName)
    {
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo
            .ToTitleCase(columnName.Replace("_", " "));
    }

    /// <summary>
    /// Refresca el DataGridView con los datos filtrados.
    /// Normaliza valores complejos (arrays, objetos) para visualización.
    /// </summary>
    private void RefreshDataGridView()
    {
        dgvData.Rows.Clear();

        int rowIndex = 0;
        foreach (var row in _processor.FilteredData)
        {
            // Crear array de strings para los valores de la fila
            var rowValues = new List<string>();

            // Añadir índice
            rowValues.Add((rowIndex + 1).ToString());

            // Añadir valores de columnas dinámicas
            foreach (var column in _processor.AvailableColumns.OrderBy(x => x))
            {
                var value = row.TryGetValue(column, out var v) ? v : null;

                // Normalizar valores complejos para visualización
                string displayValue = NormalizeValueForDisplay(value);

                rowValues.Add(displayValue);
            }

            // Agregar la fila al DataGridView
            dgvData.Rows.Add(rowValues.ToArray());
            rowIndex++;
        }
    }

    /// <summary>
    /// Normaliza valores complejos (arrays, objetos) para mostrar en el DataGridView.
    /// Convierte JArray a string separado por comas, objetos anidados a JSON comprimido.
    /// </summary>
    private string NormalizeValueForDisplay(object? value)
    {
        if (value == null)
            return "-";

        if (value is string str)
            return str;

        if (value is bool b)
            return b ? "Sí" : "No";

        if (value is int intVal)
            return intVal.ToString();

        if (value is long longVal)
            return longVal.ToString();

        if (value is decimal decVal)
            return decVal.ToString("F2");

        if (value is double doubleVal)
            return doubleVal.ToString("F2");

        if (value is Newtonsoft.Json.Linq.JArray arr)
            return string.Join(", ", arr.Select(x => x.ToString().Trim('"')));

        if (value is Newtonsoft.Json.Linq.JObject obj)
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None);

        var stringValue = value.ToString();
        return string.IsNullOrWhiteSpace(stringValue) ? "-" : stringValue;
    }

    /// <summary>
    /// Evento TextChanged del TextBox de búsqueda.
    /// Aplica filtros en tiempo real.
    /// </summary>
    private void TxtSearch_TextChanged(object? sender, EventArgs e)
    {
        if (!_isInitialized) return;
        ApplyFilters();
    }

    /// <summary>
    /// Evento CheckedChanged del CheckBox Pet Friendly.
    /// Aplica filtros en tiempo real.
    /// </summary>
    private void ChkPetFriendly_CheckedChanged(object? sender, EventArgs e)
    {
        if (!_isInitialized) return;
        ApplyFilters();
    }

    /// <summary>
    /// Evento SelectionChanged del ComboBox de tipo de propiedad.
    /// </summary>
    private void CmbPropertyType_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!_isInitialized) return;
        ApplyFilters();
    }

    /// <summary>
    /// Evento ValueChanged del NumericUpDown de precio mínimo.
    /// </summary>
    private void NudMinPrice_ValueChanged(object? sender, EventArgs e)
    {
        if (!_isInitialized) return;
        // Asegurar que el mínimo no sea mayor que el máximo
        if (nudMinPrice.Value > nudMaxPrice.Value)
            nudMaxPrice.Value = nudMinPrice.Value;
        ApplyFilters();
    }

    /// <summary>
    /// Evento ValueChanged del NumericUpDown de precio máximo.
    /// </summary>
    private void NudMaxPrice_ValueChanged(object? sender, EventArgs e)
    {
        if (!_isInitialized) return;
        // Asegurar que el máximo no sea menor que el mínimo
        if (nudMaxPrice.Value < nudMinPrice.Value)
            nudMinPrice.Value = nudMaxPrice.Value;
        ApplyFilters();
    }

    /// <summary>
    /// Evento ValueChanged del NumericUpDown de dormitorios mínimos.
    /// </summary>
    private void NudMinBedrooms_ValueChanged(object? sender, EventArgs e)
    {
        if (!_isInitialized) return;
        if (nudMinBedrooms.Value > nudMaxBedrooms.Value)
            nudMaxBedrooms.Value = nudMinBedrooms.Value;
        ApplyFilters();
    }

    /// <summary>
    /// Evento ValueChanged del NumericUpDown de dormitorios máximos.
    /// </summary>
    private void NudMaxBedrooms_ValueChanged(object? sender, EventArgs e)
    {
        if (!_isInitialized) return;
        if (nudMaxBedrooms.Value < nudMinBedrooms.Value)
            nudMinBedrooms.Value = nudMaxBedrooms.Value;
        ApplyFilters();
    }

    /// <summary>
    /// Evento ValueChanged del NumericUpDown de rating mínimo.
    /// </summary>
    private void NudMinRating_ValueChanged(object? sender, EventArgs e)
    {
        if (!_isInitialized) return;
        ApplyFilters();
    }

    /// <summary>
    /// Evento Click del botón Limpiar Filtros.
    /// Resetea todos los filtros a sus valores por defecto.
    /// </summary>
    private void BtnClearFilters_Click(object? sender, EventArgs e)
    {
        _isInitialized = false;

        txtSearch.Clear();
        chkPetFriendly.Checked = false;
        cmbPropertyType.SelectedIndex = 0;
        nudMinPrice.Value = 0;
        nudMaxPrice.Value = 100000;
        nudMinBedrooms.Value = 0;
        nudMaxBedrooms.Value = 20;
        nudMinRating.Value = 0;

        _isInitialized = true;
        ApplyFilters();
    }

    /// <summary>
    /// Aplica los filtros maestros y actualiza la visualización.
    /// </summary>
    private void ApplyFilters()
    {
        if (_processor.TotalRecordCount == 0)
            return;

        // Crear opciones de filtro con todos los valores actuales
        var options = new FilterOptions
        {
            SearchText = txtSearch.Text,
            PetFriendly = chkPetFriendly.Checked,
            PropertyType = cmbPropertyType.SelectedIndex > 0 ? cmbPropertyType.SelectedItem?.ToString() : null,
            MinPrice = nudMinPrice.Value > 0 ? (decimal)nudMinPrice.Value : null,
            MaxPrice = nudMaxPrice.Value < 100000 ? (decimal)nudMaxPrice.Value : null,
            MinBedrooms = nudMinBedrooms.Value > 0 ? (int)nudMinBedrooms.Value : null,
            MaxBedrooms = nudMaxBedrooms.Value < 20 ? (int)nudMaxBedrooms.Value : null,
            MinRating = nudMinRating.Value > 0 ? (decimal)nudMinRating.Value : null
        };

        _processor.ApplyFilters(options);
        RefreshDataGridView();
        UpdateStatus();
    }

    /// <summary>
    /// Actualiza el label de estado con información de registros.
    /// </summary>
    private void UpdateStatus()
    {
        var total = _processor.TotalRecordCount;
        var filtered = _processor.FilteredRecordCount;

        if (total == 0)
        {
            lblStatus.Text = "Cargue un archivo JSON para comenzar";
            lblStatus.ForeColor = Color.Gray;
            lblFilterInfo.Text = "Filtros activos: ninguno";
        }
        else if (filtered == total)
        {
            lblStatus.Text = $"Mostrando {filtered} de {total} registros";
            lblStatus.ForeColor = Color.Green;
            lblFilterInfo.Text = "Filtros activos: ninguno";
        }
        else
        {
            lblStatus.Text = $"Mostrando {filtered} de {total} registros (filtrados)";
            lblStatus.ForeColor = Color.Orange;
            UpdateFilterInfoLabel();
        }
    }

    /// <summary>
    /// Actualiza el label que muestra qué filtros están activos.
    /// </summary>
    private void UpdateFilterInfoLabel()
    {
        var activeFilters = new List<string>();

        if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            activeFilters.Add($"Búsqueda: \"{txtSearch.Text}\"");

        if (chkPetFriendly.Checked)
            activeFilters.Add("Pet Friendly: Sí");

        if (cmbPropertyType.SelectedIndex > 0)
            activeFilters.Add($"Tipo: {cmbPropertyType.SelectedItem}");

        if (nudMinPrice.Value > 0)
            activeFilters.Add($"Precio mín: ${nudMinPrice.Value}");

        if (nudMaxPrice.Value < 100000)
            activeFilters.Add($"Precio máx: ${nudMaxPrice.Value}");

        if (nudMinBedrooms.Value > 0)
            activeFilters.Add($"Dormitorios mín: {nudMinBedrooms.Value}");

        if (nudMaxBedrooms.Value < 20)
            activeFilters.Add($"Dormitorios máx: {nudMaxBedrooms.Value}");

        if (nudMinRating.Value > 0)
            activeFilters.Add($"Rating mín: {nudMinRating.Value}");

        lblFilterInfo.Text = activeFilters.Count > 0
            ? "Filtros activos: " + string.Join(" | ", activeFilters)
            : "Filtros activos: ninguno";
    }
}
