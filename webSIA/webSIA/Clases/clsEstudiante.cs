using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using libCnxBD;

namespace webSIA.Clases
{
    public class clsEstudiante
    {
        // ====================================================
        // Atributos
        // ====================================================
        private string strCnx;
        private string strSQL;
        private clsCnxBD objCnx;
        private SqlDataReader readerLocal;

        public int Codigo;
        public int Programa;
        public int nroDoc;
        public int tipoDoc;
        public string Nombre;
        public string Apellido;
        public bool Activo;
        public string Observacion;
        public int Respuesta;
        public string strError;

        // ====================================================
        // Punto d. Constructor sin parámetros
        // Inicializa numéricos en 0, strings vacíos y la cadena de conexión
        // ====================================================
        public clsEstudiante()
        {
            strCnx = "Data Source = .\\SQLEXPRESS; Initial Catalog = bdEstudITM; Integrated Security = SSPI;";
            Codigo = 0;
            Programa = 0;
            nroDoc = 0;
            tipoDoc = 0;
            Nombre = "";
            Apellido = "";
            Activo = false;
            Observacion = "";
            Respuesta = 0;
            strError = "";
        }

        // ====================================================
        // Punto d. Constructor con parámetros
        // Inicializa los atributos con los valores recibidos
        // ====================================================
        public clsEstudiante(int Carnet, int idPrograma, int nroDocu, int tipDocu,
                             string Nombres, string Apellidos, bool Activo, string Observ)
        {
            strCnx = "Data Source = .\\SQLEXPRESS; Initial Catalog = bdEstudITM; Integrated Security = SSPI;";
            this.Codigo = Carnet;
            this.Programa = idPrograma;
            this.nroDoc = nroDocu;
            this.tipoDoc = tipDocu;
            this.Nombre = Nombres;
            this.Apellido = Apellidos;
            this.Activo = Activo;
            this.Observacion = Observ;
            Respuesta = 0;
            strError = "";
        }

        // ====================================================
        // Punto e. Método privado: validar
        // Valida que los datos obligatorios no estén vacíos o en cero
        // ====================================================
        private bool validar()
        {
            if (Programa <= 0)
            {
                strError = "Debe seleccionar un Programa";
                return false;
            }
            if (nroDoc <= 0)
            {
                strError = "Debe ingresar el número de documento";
                return false;
            }
            if (tipoDoc <= 0)
            {
                strError = "Debe seleccionar un tipo de documento";
                return false;
            }
            if (string.IsNullOrEmpty(Nombre))
            {
                strError = "Debe ingresar el Nombre";
                return false;
            }
            if (string.IsNullOrEmpty(Apellido))
            {
                strError = "Debe ingresar el Apellido";
                return false;
            }
            return true;
        }

        // ====================================================
        // Punto f. Método público: Grabar
        // Envía los parámetros al stored procedure y ejecuta la transacción
        // Accion 1 = Grabar nuevo, Accion 2 = Modificar
        // ====================================================
        public bool Grabar(int Accion)
        {
            try
            {
                objCnx = new clsCnxBD();
                objCnx.cadCnx = strCnx;
                objCnx.SQL = strSQL;

                if (Accion == 2)
                {
                    if (!objCnx.addParam(System.Data.ParameterDirection.Input,
                        "@intCodigo", System.Data.SqlDbType.Int, 4, Codigo))
                    {
                        strError = objCnx.strError;
                        objCnx = null;
                        return false;
                    }
                }
                else if (Accion != 1)
                {
                    strError = "Acción no válida";
                    return false;
                }

                // Enviar el resto de parámetros al stored procedure
                if (!objCnx.addParam(System.Data.ParameterDirection.Input,
                    "@intIdProg", System.Data.SqlDbType.Int, 4, Programa))
                {
                    strError = objCnx.strError;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.addParam(System.Data.ParameterDirection.Input,
                    "@intNroDoc", System.Data.SqlDbType.Int, 4, nroDoc))
                {
                    strError = objCnx.strError;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.addParam(System.Data.ParameterDirection.Input,
                    "@intTipoDoc", System.Data.SqlDbType.Int, 4, tipoDoc))
                {
                    strError = objCnx.strError;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.addParam(System.Data.ParameterDirection.Input,
                    "@strNombre", System.Data.SqlDbType.VarChar, 50, Nombre))
                {
                    strError = objCnx.strError;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.addParam(System.Data.ParameterDirection.Input,
                    "@strApelli", System.Data.SqlDbType.VarChar, 50, Apellido))
                {
                    strError = objCnx.strError;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.addParam(System.Data.ParameterDirection.Input,
                    "@bitActivo", System.Data.SqlDbType.Bit, 1, Activo))
                {
                    strError = objCnx.strError;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.addParam(System.Data.ParameterDirection.Input,
                    "@strObserv", System.Data.SqlDbType.VarChar, 500, Observacion))
                {
                    strError = objCnx.strError;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.vrUnico(true))  // Ejecutar stored procedure asincrónicamente --> true
                {
                    strError = objCnx.strError;
                    objCnx = null;
                    return false;
                }

                Respuesta = Convert.ToInt32(objCnx.vrRpta);
                objCnx = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        // ====================================================
        // Punto g. Método público: buscarMaestro
        // Busca un estudiante por código usando el SP USP_Est_BuscarXcodigo
        // ====================================================
        public bool buscarMaestro(int cod)
        {
            try
            {
                if (cod <= 0)
                {
                    strError = "Carné/Código no válido";
                    return false;
                }

                strSQL = "EXEC USP_Est_BuscarXcodigo " + cod + ";";
                objCnx = new clsCnxBD();
                objCnx.cadCnx = strCnx;
                objCnx.SQL = strSQL;

                if (!objCnx.Consultar(false))  // Ejecuta sincrónicamente el método Consultar
                {
                    strError = objCnx.strError;
                    objCnx = null;
                    return false;
                }

                readerLocal = objCnx.ReaderLleno;

                if (!readerLocal.HasRows)
                {
                    strError = "No se encontró ningún registro: " + cod;
                    readerLocal.Close();
                    objCnx = null;
                    return false;
                }

                readerLocal.Read();
                Programa = readerLocal.GetInt32(0);
                nroDoc = readerLocal.GetInt32(1);
                tipoDoc = readerLocal.GetInt32(2);
                Nombre = readerLocal.GetString(3);
                Apellido = readerLocal.GetString(4);
                Activo = readerLocal.GetBoolean(5);
                Observacion = readerLocal.GetString(6);

                readerLocal.Close();
                objCnx = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        // ====================================================
        // Punto h. Método público: grabarMaestro
        // Valida los datos y ejecuta el SP USP_Est_Grabar
        // ====================================================
        public bool grabarMaestro()
        {
            if (!validar())
                return false;

            strSQL = "USP_Est_Grabar";
            return Grabar(1);
        }

        // ====================================================
        // Punto h. Método público: modificarMaestro
        // Valida los datos, verifica el código y ejecuta el SP USP_Est_Modificar
        // ====================================================
        public bool modificarMaestro()
        {
            if (!validar())
                return false;

            if (Codigo <= 0)
            {
                strError = "Nro. de Código no válido";
                return false;
            }

            strSQL = "USP_Est_Modificar";
            return Grabar(2);
        }
    }
}