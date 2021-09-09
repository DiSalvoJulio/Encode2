﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using BLL;
using System.Data.SqlClient;
using System.Data;


namespace Encode
{
    public partial class Index : System.Web.UI.Page
    {
        SuscriptorBLL suscriptorBLL = new SuscriptorBLL();

        int id = 0;
        bool nuevo = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEstado.Enabled = false;
            DeshabilitarCampos();
            btnModificar.Enabled = false;
            btnRegistrarSuscripcion.Enabled = false;
        }

        //cargamos los campos cuando buscamos un suscriptor


        //public void CargarCampos(Suscriptor suscriptor) 
        //{
        //    txtNombre.Text = suscriptor.NombreSuscriptor;
        //    txtApellido.Text = suscriptor.ApellidoSuscriptor;
        //    txtDocumento.Text = suscriptor.NumeroDocumento;
        //    cboTipoDoc.SelectedValue = suscriptor.TipoDocumento;
        //    txtDireccion.Text = suscriptor.Direccion;
        //    txtTelefono.Text = suscriptor.NroTelefono;
        //    txtEmail.Text = suscriptor.Email;
        //    txtNombreUsuario.Text = suscriptor.NombreUsuario;
        //    txtContrasenia.Text = suscriptor.Contrasenia;      

        //}

        public void BuscarSuscriptor(string tipoDoc, string nroDoc)
        {
            Suscriptor suscriptor = suscriptorBLL.BuscarSuscriptor(tipoDoc, nroDoc);
            if (suscriptor == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "MyFunction", "MsjNoSeEncontroSuscriptor();", true);
                return;
            }

            if (tipoDoc == suscriptor.TipoDocumento && nroDoc == suscriptor.NumeroDocumento)
            {
                id = suscriptor.IdSuscriptor;
                ViewState["idSuscriptor"] = id;
                txtNombre.Text = suscriptor.NombreSuscriptor;
                txtApellido.Text = suscriptor.ApellidoSuscriptor;
                txtDireccion.Text = suscriptor.Direccion;
                txtEmail.Text = suscriptor.Email;
                txtTelefono.Text = suscriptor.NroTelefono;
                txtNombreUsuario.Text = suscriptor.NombreUsuario;
                txtContrasenia.Text = suscriptor.Contrasenia;

                DeshabilitarCampos();
                btnModificar.Enabled = true;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cboTipoDoc.SelectedIndex.Equals(0) || txtDocumento.Text.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "swal('Debe completar Tipo y Numero de Documento del suscriptor')", true);
            }
            else
            {
                BuscarSuscriptor(cboTipoDoc.SelectedValue, txtDocumento.Text);
                DeshabilitarCampos();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarCampos();
            LimpiarCampos();

            nuevo = true;
            ViewState["variableNuevo"] = nuevo;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "swal('Esta por modificar un suscriptor')", true);
            cboTipoDoc.Enabled = false;
            txtDocumento.Enabled = false;
            HabilitarCampos();
            nuevo = false;
            ViewState["variableNuevo"] = nuevo;
        }

        public bool Insertar(string nombre, string apellido, string numeroDocumento, string tipoDocumento, string direccion, string email, string telefono, string nombreUsuario, string pass)
        {
            SuscriptorBLL suscriptorBLL = new SuscriptorBLL();
            Suscriptor suscriptor = new Suscriptor();

            suscriptor.NombreSuscriptor = nombre;
            suscriptor.ApellidoSuscriptor = apellido;
            suscriptor.Direccion = direccion;
            suscriptor.NumeroDocumento = numeroDocumento;
            suscriptor.TipoDocumento = tipoDocumento;
            suscriptor.Direccion = direccion;
            suscriptor.Email = email;
            suscriptor.NroTelefono = telefono;
            suscriptor.NombreUsuario = nombreUsuario;
            suscriptor.Contrasenia = pass;
            return suscriptorBLL.Insertar(suscriptor);
        }

        public bool Modificar(string nombre, string apellido, string tipoDocumento, string direccion, string email, string telefono, string pass)
        {
            SuscriptorBLL suscriptorBLL = new SuscriptorBLL();
            Suscriptor suscriptor = new Suscriptor();

            suscriptor.NombreSuscriptor = nombre;
            suscriptor.ApellidoSuscriptor = apellido;
            suscriptor.Direccion = direccion;            
            suscriptor.TipoDocumento = tipoDocumento;
            suscriptor.Direccion = direccion;
            suscriptor.Email = email;
            suscriptor.NroTelefono = telefono;            
            suscriptor.Contrasenia = pass;
            return suscriptorBLL.ModificarSuscriptor(suscriptor);
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (nuevo)
            {
                Insertar(txtNombre.Text, txtApellido.Text, txtDocumento.Text, cboTipoDoc.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text, txtNombreUsuario.Text, txtContrasenia.Text);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "swal('Datos cargados con exito!')", true);
            }
            else
            {
                Modificar(txtNombre.Text, txtApellido.Text, cboTipoDoc.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text, txtContrasenia.Text);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "swal('Modificacion con exito!')", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarSuscripcion_Click(object sender, EventArgs e)
        {

        }

        //public void ModificarSuscriptor()
        //{

        //}

        public void DeshabilitarCampos()
        {
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDireccion.Enabled = false;
            txtEmail.Enabled = false;
            txtTelefono.Enabled = false;
            txtNombreUsuario.Enabled = false;
            txtContrasenia.Enabled = false;
        }

        public void HabilitarCampos()
        {
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDireccion.Enabled = true;
            txtEmail.Enabled = true;
            txtTelefono.Enabled = true;
            txtNombreUsuario.Enabled = true;
            txtContrasenia.Enabled = true;
        }

        public void LimpiarCampos()
        {
            cboTipoDoc.SelectedIndex = 0;
            txtDocumento.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtNombreUsuario.Text = "";
            txtContrasenia.Text = "";
        }

    }
}