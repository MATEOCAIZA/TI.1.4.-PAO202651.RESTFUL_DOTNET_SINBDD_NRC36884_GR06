using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ec.edu.monster.controlador;

namespace ec.edu.monster.vista
{
    public partial class Form1 : Form, IConversionView
    {
        private ConversionController _controller;

        private Panel _loginRootPanel;
        private Panel _contentPanel;
        private PictureBox _loginPictureBox;
        private TextBox _usuarioTextBox;
        private TextBox _contrasenaTextBox;
        private Label _loginMessageLabel;
        private Button _loginButton;
        private ComboBox _categoriaComboBox;
        private ComboBox _origenComboBox;
        private ComboBox _destinoComboBox;
        private TextBox _valorTextBox;
        private Label _resultadoLabel;
        private Button _convertButton;
        private Button _clearButton;

        public Form1()
        {
            InitializeComponent();
            InitializeViewControls();
            BuildLayout();

            _controller = new ConversionController(this);
            _controller.Iniciar();
        }

        public Form1(ConversionController controller)
        {
            InitializeComponent();
            InitializeViewControls();
            BuildLayout();

            _controller = controller ?? new ConversionController(this);
            _controller.Iniciar();
        }

        public void SetController(ConversionController controller)
        {
            _controller = controller;
        }

        private void InitializeViewControls()
        {
            _loginRootPanel = new Panel();
            _contentPanel = new Panel();
            _loginPictureBox = new PictureBox();
            _usuarioTextBox = new TextBox();
            _contrasenaTextBox = new TextBox();
            _loginMessageLabel = new Label();
            _loginButton = new Button();
            _categoriaComboBox = new ComboBox();
            _origenComboBox = new ComboBox();
            _destinoComboBox = new ComboBox();
            _valorTextBox = new TextBox();
            _resultadoLabel = new Label();
            _convertButton = new Button();
            _clearButton = new Button();
        }

        private void BuildLayout()
        {
            Text = "Monster App - Conversor de Unidades";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(980, 560);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackColor = Color.FromArgb(245, 246, 250); // Fondo claro moderno

            _contentPanel = BuildContentPanel();
            _loginRootPanel = BuildLoginPanel();

            Controls.Add(_contentPanel);
            Controls.Add(_loginRootPanel);
        }

        private Panel BuildLoginPanel()
        {
            var root = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(44, 62, 80) // Fondo oscuro azulado
            };

            // Tarjeta central para el login
            var card = new Panel
            {
                Size = new Size(380, 460),
                Location = new Point(300, 50),
                BackColor = Color.White,
                Padding = new Padding(30)
            };

            _loginPictureBox.Size = new Size(120, 120);
            _loginPictureBox.Location = new Point(130, 20);
            _loginPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            _loginPictureBox.Image = LoadLoginImage();

            var title = new Label
            {
                Text = "BIENVENIDO",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(110, 155)
            };

            var userLabel = new Label
            {
                Text = "Usuario",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(40, 210)
            };

            _usuarioTextBox.Location = new Point(40, 235);
            _usuarioTextBox.Size = new Size(300, 30);
            _usuarioTextBox.Font = new Font("Segoe UI", 12);
            _usuarioTextBox.BorderStyle = BorderStyle.FixedSingle;

            var passwordLabel = new Label
            {
                Text = "Contraseña",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(40, 280)
            };

            _contrasenaTextBox.Location = new Point(40, 305);
            _contrasenaTextBox.Size = new Size(300, 30);
            _contrasenaTextBox.Font = new Font("Segoe UI", 12);
            _contrasenaTextBox.BorderStyle = BorderStyle.FixedSingle;
            _contrasenaTextBox.UseSystemPasswordChar = true;

            _loginButton.Text = "INGRESAR";
            _loginButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            _loginButton.Size = new Size(300, 45);
            _loginButton.Location = new Point(40, 360);
            _loginButton.FlatStyle = FlatStyle.Flat;
            _loginButton.BackColor = Color.FromArgb(41, 128, 185);
            _loginButton.ForeColor = Color.White;
            _loginButton.FlatAppearance.BorderSize = 0;
            _loginButton.Cursor = Cursors.Hand;
            _loginButton.Click += LoginButton_Click;

            _loginMessageLabel.AutoSize = false;
            _loginMessageLabel.Location = new Point(40, 415);
            _loginMessageLabel.Size = new Size(300, 35);
            _loginMessageLabel.Font = new Font("Segoe UI", 9);
            _loginMessageLabel.ForeColor = Color.Firebrick;
            _loginMessageLabel.TextAlign = ContentAlignment.MiddleCenter;

            card.Controls.Add(_loginPictureBox);
            card.Controls.Add(title);
            card.Controls.Add(userLabel);
            card.Controls.Add(_usuarioTextBox);
            card.Controls.Add(passwordLabel);
            card.Controls.Add(_contrasenaTextBox);
            card.Controls.Add(_loginButton);
            card.Controls.Add(_loginMessageLabel);

            root.Controls.Add(card);

            return root;
        }

        private Panel BuildContentPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 246, 250),
                Visible = false
            };

            // Banner Superior
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(41, 128, 185)
            };

            var titleLabel = new Label
            {
                Text = "Conversión de Unidades",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(30, 20)
            };

            var subtitle = new Label
            {
                Text = "ec.edu.monster.servicio",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.LightGray,
                AutoSize = true,
                Location = new Point(380, 30)
            };

            header.Controls.Add(titleLabel);
            header.Controls.Add(subtitle);

            // Controles de formulario (Estilo Tarjeta Blanca)
            var card = new Panel
            {
                Size = new Size(800, 400),
                Location = new Point(90, 120),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Font labelFont = new Font("Segoe UI", 12, FontStyle.Bold);
            Font inputFont = new Font("Segoe UI", 12);

            var lblCategoria = new Label { Text = "Categoría:", Font = labelFont, Location = new Point(230, 40), AutoSize = true, ForeColor = Color.FromArgb(44, 62, 80) };
            _categoriaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _categoriaComboBox.Font = inputFont;
            _categoriaComboBox.Size = new Size(300, 30);
            _categoriaComboBox.Location = new Point(330, 37);
            _categoriaComboBox.SelectedIndexChanged += (s, e) =>
            {
                var seleccion = _categoriaComboBox.SelectedItem?.ToString();
                _controller.ActualizarUnidades(seleccion);
            };

            var lblValor = new Label { Text = "Valor a convertir:", Font = labelFont, Location = new Point(50, 110), AutoSize = true, ForeColor = Color.FromArgb(44, 62, 80) };
            _valorTextBox.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            _valorTextBox.Size = new Size(700, 36);
            _valorTextBox.Location = new Point(50, 140);
            _valorTextBox.Text = "0";
            _valorTextBox.TextAlign = HorizontalAlignment.Center;

            var lblOrigen = new Label { Text = "Unidad Origen:", Font = labelFont, Location = new Point(50, 200), AutoSize = true, ForeColor = Color.FromArgb(44, 62, 80) };
            _origenComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _origenComboBox.Font = inputFont;
            _origenComboBox.Size = new Size(320, 30);
            _origenComboBox.Location = new Point(50, 230);

            var lblDestino = new Label { Text = "Unidad Destino:", Font = labelFont, Location = new Point(430, 200), AutoSize = true, ForeColor = Color.FromArgb(44, 62, 80) };
            _destinoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _destinoComboBox.Font = inputFont;
            _destinoComboBox.Size = new Size(320, 30);
            _destinoComboBox.Location = new Point(430, 230);

            _convertButton = new Button
            {
                Text = "CONVERTIR",
                Location = new Point(50, 310),
                Size = new Size(200, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(39, 174, 96), // Verde esmeralda
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _convertButton.FlatAppearance.BorderSize = 0;
            _convertButton.Click += ConvertButton_Click;

            _clearButton = new Button
            {
                Text = "LIMPIAR",
                Location = new Point(270, 310),
                Size = new Size(130, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(149, 165, 166), // Gris
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _clearButton.FlatAppearance.BorderSize = 0;
            _clearButton.Click += ClearButton_Click;

            _resultadoLabel = new Label
            {
                Location = new Point(430, 310),
                Size = new Size(320, 50),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoEllipsis = true,
                Text = "Resultado: ",
                TextAlign = ContentAlignment.MiddleLeft
            };

            card.Controls.Add(lblCategoria);
            card.Controls.Add(_categoriaComboBox);
            card.Controls.Add(lblValor);
            card.Controls.Add(_valorTextBox);
            card.Controls.Add(lblOrigen);
            card.Controls.Add(_origenComboBox);
            card.Controls.Add(lblDestino);
            card.Controls.Add(_destinoComboBox);
            card.Controls.Add(_convertButton);
            card.Controls.Add(_clearButton);
            card.Controls.Add(_resultadoLabel);

            panel.Controls.Add(header);
            panel.Controls.Add(card);

            return panel;
        }

        #region Event Handlers delegating to Controller

        private void LoginButton_Click(object sender, EventArgs e)
        {
            _controller.IniciarSesion(_usuarioTextBox.Text, _contrasenaTextBox.Text);
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            var categoria = _categoriaComboBox.SelectedItem?.ToString();
            var origen = _origenComboBox.SelectedItem?.ToString();
            var destino = _destinoComboBox.SelectedItem?.ToString();
            var valor = _valorTextBox.Text;

            _controller.Convertir(categoria, origen, destino, valor);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _controller.Limpiar();
        }

        #endregion

        #region IConversionView Implementation

        public void CargarCategorias(string[] categorias)
        {
            _categoriaComboBox.Items.Clear();
            if (categorias != null && categorias.Length > 0)
            {
                _categoriaComboBox.Items.AddRange(categorias);
                _categoriaComboBox.SelectedIndex = 0;
            }
        }

        public void CargarUnidades(string[] unidades)
        {
            _origenComboBox.Items.Clear();
            _destinoComboBox.Items.Clear();

            if (unidades != null && unidades.Length > 0)
            {
                _origenComboBox.Items.AddRange(unidades);
                _destinoComboBox.Items.AddRange(unidades);
                _origenComboBox.SelectedIndex = 0;
                _destinoComboBox.SelectedIndex = 0;
            }
        }

        public void MostrarPantallaLogin()
        {
            _loginRootPanel.Visible = true;
            _contentPanel.Visible = false;
            AcceptButton = _loginButton;
            Text = "Iniciar sesión - Monster App";
            _usuarioTextBox.Clear();
            _contrasenaTextBox.Clear();
            _loginMessageLabel.Text = "";
            _usuarioTextBox.Focus();
        }

        public void MostrarPantallaPrincipal()
        {
            _loginRootPanel.Visible = false;
            _contentPanel.Visible = true;
            _contentPanel.BringToFront();
            AcceptButton = _convertButton;
            Text = "Conversor de Unidades";
        }

        public void MostrarMensajeLogin(string mensaje, bool esError)
        {
            _loginMessageLabel.ForeColor = esError ? Color.Firebrick : Color.DarkGreen;
            _loginMessageLabel.Text = mensaje ?? string.Empty;

            if (esError)
            {
                _contrasenaTextBox.Clear();
                _contrasenaTextBox.Focus();
            }
        }

        public void MostrarResultado(string mensaje, bool esExito)
        {
            _resultadoLabel.ForeColor = esExito ? Color.FromArgb(39, 174, 96) : Color.Firebrick;
            _resultadoLabel.Text = mensaje ?? string.Empty;
        }

        public void LimpiarCamposConversion()
        {
            if (_categoriaComboBox.Items.Count > 0)
            {
                _categoriaComboBox.SelectedIndex = 0;
            }
            _valorTextBox.Text = "0";
            _resultadoLabel.ForeColor = Color.FromArgb(44, 62, 80);
            _resultadoLabel.Text = "Resultado:";
        }

        #endregion

        private static Image LoadLoginImage()
        {
            var imagePath = Path.Combine(Application.StartupPath, "monsterLogin.jpg");
            if (File.Exists(imagePath))
            {
                return Image.FromFile(imagePath);
            }

            return null;
        }
    }
}