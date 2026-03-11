using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace libCnxBD
{
    public class clsCnxBD
    {
        // Atributos
        private bool blnHayCnx;
        private SqlConnection objCnx;
        private SqlCommand objCmd;
        private SqlParameter objParam;
        public string SQL;
        public string cadCnx;
        public SqlDataReader ReaderLleno;
        public object vrRpta;
        public string strError;

        // ====================================================
        // Punto d. Constructor
        // Inicializa los objetos de conexión, comando y parámetro
        // ====================================================
        public clsCnxBD()
        {
            blnHayCnx = false;
            objCnx = new SqlConnection();
            objCmd = new SqlCommand();
            objParam = new SqlParameter();
        }

        // ====================================================
        // Punto e. Método privado: abrirCnx
        // Abre la conexión a la BD usando la cadena cadCnx
        // ====================================================
        private bool abrirCnx()
        {
            if (string.IsNullOrEmpty(cadCnx))
                return false;
            objCnx.ConnectionString = cadCnx;
            try
            {
                objCnx.Open();
                blnHayCnx = true;
                return true;
            }
            catch (Exception ex)
            {
                strError = "Error, reintentar";
                blnHayCnx = false;
                return false;
            }
        }

        // ====================================================
        // Punto f. Método público: cerrarCnx
        // Cierra y libera la conexión a la BD
        // ====================================================
        public void cerrarCnx()
        {
            try
            {
                objCnx.Close();
                objCnx = null;
                blnHayCnx = false;
            }
            catch
            {
                strError = "Problemas en cierre o liberación de la conexión.";
            }
        }

        // ====================================================
        // Punto g. Método público: Consultar
        // Ejecuta una consulta y almacena el resultado en ReaderLleno
        // (true = Stored Procedure, false = SQL directo)
        // ====================================================
        public bool Consultar(bool blnTipoEjec)
        {
            try
            {
                if (string.IsNullOrEmpty(SQL) || string.IsNullOrEmpty(cadCnx))
                {
                    strError = "Faltan datos";
                    return false;
                }
                if (!blnHayCnx)
                    if (!abrirCnx())
                        return false;

                objCmd.Connection = objCnx;
                objCmd.CommandText = SQL;
                if (blnTipoEjec)
                    objCmd.CommandType = CommandType.StoredProcedure;
                else
                    objCmd.CommandType = CommandType.Text;

                ReaderLleno = objCmd.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        // ====================================================
        // Punto h. Método público: vrUnico
        // Ejecuta una consulta que retorna un único valor y lo guarda en vrRpta
        // (true = Stored Procedure, false = SQL directo)
        // ====================================================
        public bool vrUnico(bool accion)
        {
            try
            {
                if (string.IsNullOrEmpty(SQL) || string.IsNullOrEmpty(cadCnx))
                {
                    strError = "Faltan datos";
                    return false;
                }
                if (!blnHayCnx)
                    if (!abrirCnx())
                        return false;

                objCmd.Connection = objCnx;
                objCmd.CommandText = SQL;
                if (accion)
                    objCmd.CommandType = CommandType.StoredProcedure;
                else
                    objCmd.CommandType = CommandType.Text;

                vrRpta = objCmd.ExecuteScalar();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        // ====================================================
        // Punto i. Método público: execCmd
        // Ejecuta comandos sin retorno de datos: INSERT, UPDATE, DELETE
        // (true = Stored Procedure, false = SQL directo)
        // ====================================================
        public bool execCmd(bool accion)
        {
            try
            {
                if (string.IsNullOrEmpty(SQL) || string.IsNullOrEmpty(cadCnx))
                {
                    strError = "Faltan datos";
                    return false;
                }
                if (!blnHayCnx)
                    if (!abrirCnx())
                        return false;

                objCmd.Connection = objCnx;
                objCmd.CommandText = SQL;
                if (accion)
                    objCmd.CommandType = CommandType.StoredProcedure;
                else
                    objCmd.CommandType = CommandType.Text;

                objCmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        // ====================================================
        // Punto j. Método público: addParam
        // Agrega un parámetro al comando SQL para enviarlo al stored procedure
        // ====================================================
        public bool addParam(ParameterDirection Direc, string Nombre, SqlDbType tipoDato,
                             Int16 Tam, object Vr)
        {
            try
            {
                objParam.Direction = Direc;
                objParam.ParameterName = Nombre;
                objParam.SqlDbType = tipoDato;
                objParam.Size = Tam;
                objParam.Value = Vr;
                objCmd.Parameters.Add(objParam);
                objParam = new SqlParameter();
                return true;
            }
            catch
            {
                strError = "Error en ingreso de dato ";
                return false;
            }
        }
    }
}