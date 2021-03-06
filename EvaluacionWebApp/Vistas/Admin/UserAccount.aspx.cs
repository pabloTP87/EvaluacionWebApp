﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvaluacionWebApp.Logica.ModeloEntidades;
using EvaluacionWebApp.Logica.Clases;

namespace EvaluacionWebApp.Vistas.Admin
{
    public partial class UserAccount : System.Web.UI.Page
    {
        /**
         * Page Load carga dropdownlist con los roles de usuario de la entidad roles de la base de datos
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using(db_nutricionEntities dbEntity=new db_nutricionEntities())
                {
                    lsdRol.DataSource = dbEntity.Roles.ToList();
                    lsdRol.DataTextField = "rol";
                    lsdRol.DataValueField = "id_rol";
                    lsdRol.DataBind();
                }
            }

            txtNombreUsuario.Attributes.Add("class", "validate");
            txtNombreUsuario.Attributes.Add("pattern", "^\\s*[a-zA-ZñÑ\\s]+\\s*$");
            txtNombreUsuario.Attributes.Add("title", "Se admiten solo letras");

            txtApepat.Attributes.Add("class", "validate");
            txtApepat.Attributes.Add("pattern", "^\\s*[a-zA-ZñÑ\\s]+\\s*$");
            txtApepat.Attributes.Add("title", "Se admiten solo letras");

            txtApemat.Attributes.Add("class", "validate");
            txtApemat.Attributes.Add("pattern", "^\\s*[a-zA-ZñÑ\\s]+\\s*$");
            txtApemat.Attributes.Add("title", "Se admiten solo letras");

        }
        /**
         * Evento Click para guardar una cuenta de usuario en el sistema
         * al confirmar se muestra una ventana modal con un mensaje de confirmación
         */
        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            clsUsuario usuario = new clsUsuario();
            
            String nombreUsaurio = txtNombreUsuario.Text;
            String apepat = txtApepat.Text;
            String apemat = txtApemat.Text;
            DateTime fecha = DateTime.Now;
            String login = txtLogin.Text;
            String contraseña = txtContraseña.Text;
            int idRol = int.Parse(lsdRol.SelectedValue);

            String rut = usuario.validarRut(txtRutUsuario.Text);

            if(rut.Equals("Rut invalido"))
            {
                lblRutInvalido.Text = rut;

            }
            else
            {
                lblRutInvalido.Text = "";
            }

            if (nombreUsaurio=="" || apepat=="" || apemat=="" || login=="" || contraseña=="" || idRol==0 || rut.Equals("Rut invalido"))
            {
                lblResultado.Text = "Complete los campos requeridos*";
            }
            else
            {
                String mensaje = usuario.guardarUsuario(nombreUsaurio, Convert.ToInt32(rut), apepat, apemat, fecha, login, contraseña, idRol);

                if(mensaje.Equals("El rut ingresado ya existe"))
                {
                    lblConfirmacion.Text = mensaje;
                    mpeConfirmar.Show();
                }
                else
                {
                    lblConfirmacion.Text = mensaje;
                    mpeConfirmar.Show();
                }
            }
        }
        /**
         * Evento Click del boton confirmar de la ventana modal con mensaje de exito de operación de insercíón del usuario
         */
        public void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Evaluamos mensaje devuelto por el metodo guardarUsuario(), solo si guarda corectamente, limpiamos el formulario.
            if(lblConfirmacion.Text.Equals("El rut ingresado ya existe"))
            {
                mpeConfirmar.Hide();
            }
            else
            {
                mpeConfirmar.Hide();
                limpiar();
            }
        }

        /**
        * Metodo que limpia las cajas de texto y otros controles del formulario de ingreso de usaurio
        */
        public void limpiar()
        {
            txtNombreUsuario.Text = "";
            txtRutUsuario.Text = "";
            txtApepat.Text = "";
            txtApemat.Text = "";
            txtLogin.Text = "";
            txtContraseña.Text = "";
            lsdRol.SelectedIndex = 0;
            lblResultado.Text = "";
        }
    }
}