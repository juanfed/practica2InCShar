using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webSIA.Clases;

namespace webSIA
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // ====================================================
        // Actividad 6a. Variable global static para la opcion del menu
        // 1 = Agregar, 2 = Modificar
        // ====================================================
        static int intOpcion;

        // ====================================================
        // Actividad 6b. Variables para cada dato requerido
        // ====================================================
        string strNombre, strApellido, strObservac;
        int intCod, intNroDoc, intTipDoc, intPrograma;
        bool blnAct;

        // ====================================================
        // Actividad 6c. Mensaje: muestra texto en el label lblMsj
        // ====================================================
        private void Mensaje(string txt)
        {
            IBIMsj.Text = txt;
        }

        // ====================================================
        // Actividad 6c. llenarComboTipoDoc: llena el combo de tipo de documento
        // ====================================================
        private void llenarComboTipoDoc()
        {
            ddITDoc.Items.Clear();
            ddITDoc.Items.Add(new ListItem("Seleccione...", "0"));
            ddITDoc.Items.Add(new ListItem("Cedula Ciudadania", "1"));
            ddITDoc.Items.Add(new ListItem("Cedula Extranjeria", "3"));
            ddITDoc.Items.Add(new ListItem("Pasaporte", "6"));
            ddITDoc.Items.Add(new ListItem("NIT", "7"));
            ddITDoc.Items.Add(new ListItem("NUIP", "8"));
            ddITDoc.Items.Add(new ListItem("Permiso de Proteccion Temporal", "9"));
        }

        // ====================================================
        // Actividad 6c. llenarComboProg: llena el combo de programas
        // ====================================================
        private void llenarComboProg()
        {
            ddlProg.Items.Clear();
            ddlProg.Items.Add(new ListItem("Seleccione...", "0"));
            ddlProg.Items.Add(new ListItem("Tecnologia en Informatica Musical", "10"));
            ddlProg.Items.Add(new ListItem("Ingenieria en Diseno Industrial", "11"));
            ddlProg.Items.Add(new ListItem("Quimica Industrial", "70"));
            ddlProg.Items.Add(new ListItem("Ingenieria Biomedica", "71"));
            ddlProg.Items.Add(new ListItem("Tecn. Desarrollo de Software", "100"));
            ddlProg.Items.Add(new ListItem("Ing. de Sistemas", "101"));
        }

        // ====================================================
        // Actividad 6c. Limpiar: borra/posiciona la informacion en cada objeto del formulario
        // ====================================================
        private void Limpiar()
        {
            txtCodi.Text = "";
            ddlProg.SelectedIndex = 0;
            txtNDoc.Text = "";
            ddITDoc.SelectedIndex = 0;
            txtNomb.Text = "";
            txtApel.Text = "";
            chkActi.Checked = false;
            txtObse.Text = "";
            Mensaje("");
        }

        // ====================================================
        // Actividad 6c. Buscar: busca un estudiante por codigo y muestra los datos
        // ====================================================
        private void Buscar(int Codigo)
        {
            clsEstudiante objEst = new clsEstudiante();

            if (!objEst.buscarMaestro(Codigo))
            {
                Mensaje(objEst.strError);
                return;
            }

            // Mostrar los datos encontrados en el formulario
            txtCodi.Text = Codigo.ToString();
            ddlProg.SelectedValue = objEst.Programa.ToString();
            txtNDoc.Text = objEst.nroDoc.ToString();
            ddITDoc.SelectedValue = objEst.tipoDoc.ToString();
            txtNomb.Text = objEst.Nombre;
            txtApel.Text = objEst.Apellido;
            chkActi.Checked = objEst.Activo;
            txtObse.Text = objEst.Observacion;

            Mensaje("Registro encontrado.");
        }

        // ====================================================
        // Actividad 6c. Grabar: captura datos del formulario y ejecuta la transaccion
        // ====================================================
        private void Grabar()
        {
            // Capturar datos del formulario
            intPrograma = Convert.ToInt32(ddlProg.SelectedValue);
            intNroDoc = (txtNDoc.Text.Trim() != "") ? Convert.ToInt32(txtNDoc.Text.Trim()) : 0;
            intTipDoc = Convert.ToInt32(ddITDoc.SelectedValue);
            strNombre = txtNomb.Text.Trim();
            strApellido = txtApel.Text.Trim();
            blnAct = chkActi.Checked;
            strObservac = txtObse.Text.Trim();

            if (intOpcion == 1) // Agregar
            {
                clsEstudiante objEst = new clsEstudiante(0, intPrograma, intNroDoc, intTipDoc,
                    strNombre, strApellido, blnAct, strObservac);

                if (!objEst.grabarMaestro())
                {
                    Mensaje(objEst.strError);
                    return;
                }

                if (objEst.Respuesta == -1)
                {
                    Mensaje("El numero de documento ya existe.");
                }
                else if (objEst.Respuesta == 0)
                {
                    Mensaje("Error al grabar el registro.");
                }
                else
                {
                    Mensaje("Registro grabado exitosamente. Codigo asignado: " + objEst.Respuesta);
                    txtCodi.Text = objEst.Respuesta.ToString();
                }
            }
            else if (intOpcion == 2) // Modificar
            {
                intCod = Convert.ToInt32(txtCodi.Text.Trim());

                clsEstudiante objEst = new clsEstudiante(intCod, intPrograma, intNroDoc, intTipDoc,
                    strNombre, strApellido, blnAct, strObservac);

                if (!objEst.modificarMaestro())
                {
                    Mensaje(objEst.strError);
                    return;
                }

                if (objEst.Respuesta == -1)
                {
                    Mensaje("El numero de documento ya existe en otro registro.");
                }
                else if (objEst.Respuesta == 0)
                {
                    Mensaje("Error al modificar el registro.");
                }
                else
                {
                    Mensaje("Registro modificado exitosamente.");
                }
            }
        }

        // ====================================================
        // Actividad 6d. Page_Load: carga inicial del formulario
        // ====================================================
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenarComboProg();
                llenarComboTipoDoc();
                Limpiar();
                intOpcion = 0;
            }
        }

        // ====================================================
        // Actividad 6d. Menu1_MenuItemClick: maneja las opciones del menu
        // ====================================================
        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "opcBusc":
                    Limpiar();
                    txtCodi.ReadOnly = false;
                    Mensaje("Ingrese el codigo/carne y presione el boton de busqueda.");
                    break;

                case "opcAgre":
                    intOpcion = 1;
                    Limpiar();
                    txtCodi.ReadOnly = true;
                    txtCodi.Text = "(Nuevo)";
                    Mensaje("Ingrese los datos del nuevo estudiante y presione Grabar.");
                    break;

                case "opcModi":
                    if (txtCodi.Text.Trim() == "" || txtCodi.Text.Trim() == "(Nuevo)")
                    {
                        Mensaje("Primero debe buscar un registro para poder modificarlo.");
                        return;
                    }
                    intOpcion = 2;
                    txtCodi.ReadOnly = true;
                    Mensaje("Modifique los datos y presione Grabar.");
                    break;

                case "opcGrab":
                    if (intOpcion == 0)
                    {
                        Mensaje("Seleccione primero Agregar o Modificar.");
                        return;
                    }
                    Grabar();
                    break;

                case "opcCanc":
                    Limpiar();
                    txtCodi.ReadOnly = true;
                    intOpcion = 0;
                    Mensaje("Operacion cancelada.");
                    break;
            }
        }

        // ====================================================
        // Actividad 6d. ibtnBusc_Click: boton de busqueda (lupa)
        // ====================================================
        protected void ibtnBusc_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCodi.Text.Trim() == "" || txtCodi.Text.Trim() == "(Nuevo)")
            {
                Mensaje("Ingrese un codigo/carne valido.");
                return;
            }

            int codigo;
            if (!int.TryParse(txtCodi.Text.Trim(), out codigo))
            {
                Mensaje("El codigo debe ser numerico.");
                return;
            }

            Buscar(codigo);
            txtCodi.ReadOnly = true;
        }
    }
}
