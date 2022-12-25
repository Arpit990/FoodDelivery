using FoodDelivery.Model;
using System.Data;
using System.Data.SqlClient;

namespace FoodDelivery.Services
{
    public class dbServices
    {
        public static string ConnectionString = "Data Source=DESKTOP-CP4OIIC\\SQLEXPRESS;Initial Catalog=FoodDelivery;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public User getUser(LoginModel userModel)
        {
            var Message = "";
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                objConn.Open();
                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "UR_User_Select";
                        //objCmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        objCmd.Parameters.AddWithValue("@Username", SqlDbType.VarChar).Value = userModel.Username;
                        objCmd.Parameters.AddWithValue("@Password", SqlDbType.VarChar).Value = userModel.Password;

                        #endregion Prepare Command

                        objCmd.ExecuteNonQuery();

                        //if (objCmd.Parameters["@Id"] != null)
                        //    userModel.UserID = Convert.ToInt32(objCmd.Parameters["@Id"].Value);

                        #region ReadData and Set Controls
                        User entUser = new User();
                        using (SqlDataReader objSDR = objCmd.ExecuteReader())
                        {
                            while (objSDR.Read())
                            {
                                if (!objSDR["Id"].Equals(DBNull.Value))
                                    entUser.Id = Convert.ToInt32(objSDR["Id"]);
                                if (!objSDR["Username"].Equals(DBNull.Value))
                                    entUser.UserName = objSDR["Username"].ToString();
                                if (!objSDR["Password"].Equals(DBNull.Value))
                                    entUser.Password = objSDR["Password"].ToString();
                            }
                        }

                        return entUser;
                        #endregion ReadData and Set Controls
                    }
                    
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.Message.ToString();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.Message.ToString();
                        return null;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }

        public bool addCartItem(CartModel model)
        {
            var Message = "";
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                objConn.Open();
                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "CR_Cart_Insert";
                        objCmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = model.UserId;
                        objCmd.Parameters.AddWithValue("@FoodId", SqlDbType.Int).Value = model.FoodId;
                        objCmd.Parameters.AddWithValue("@Count", SqlDbType.Int).Value = model.Count;

                        #endregion Prepare Command

                        objCmd.ExecuteNonQuery();

                        return true;
                    }

                    catch (SqlException sqlex)
                    {
                        Message = sqlex.Message.ToString();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.Message.ToString();
                        return false;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }

        public bool removeCartIteam(int UserId,int ItemId)
        {
            var Message = "";
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                objConn.Open();
                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "CR_Cart_Insert";
                        objCmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = ItemId;
                        objCmd.Parameters.AddWithValue("@UserId", SqlDbType.VarChar).Value = UserId;

                        #endregion Prepare Command

                        objCmd.ExecuteNonQuery();

                        return true;
                    }

                    catch (SqlException sqlex)
                    {
                        Message = sqlex.Message.ToString();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.Message.ToString();
                        return false;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }

        public bool deliveryDetails(AddressModel model)
        {
            var Message = "";
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                objConn.Open();
                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "AD_DeliveryDetails_Insert";
                        objCmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = model.UserId;
                        objCmd.Parameters.AddWithValue("@MobileNo", SqlDbType.VarChar).Value = model.MobileNo;
                        objCmd.Parameters.AddWithValue("@Address", SqlDbType.VarChar).Value = model.Address;
                        objCmd.Parameters.AddWithValue("@AddressType", SqlDbType.VarChar).Value = model.Address;
                        objCmd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = model.Address;

                        #endregion Prepare Command

                        objCmd.ExecuteNonQuery();

                        return true;
                    }

                    catch (SqlException sqlex)
                    {
                        Message = sqlex.Message.ToString();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.Message.ToString();
                        return false;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }

        public List<Cart> getCartDetail(int UserId)
        {
            var Message = "";
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                objConn.Open();
                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "CR_Cart_Select";
                        //objCmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        objCmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = UserId;

                        #endregion Prepare Command

                        objCmd.ExecuteNonQuery();

                        //if (objCmd.Parameters["@Id"] != null)
                        //    userModel.UserID = Convert.ToInt32(objCmd.Parameters["@Id"].Value);

                        #region ReadData and Set Controls
                        List<Cart> cartList = new List<Cart>();
                        using (SqlDataReader objSDR = objCmd.ExecuteReader())
                        {
                            while (objSDR.Read())
                            {
                                Cart cr = new Cart();
                                if (!objSDR["Id"].Equals(DBNull.Value))
                                    cr.Id = Convert.ToInt32(objSDR["Id"]);
                                if (!objSDR["UserId"].Equals(DBNull.Value))
                                    cr.UserId = Convert.ToInt32(objSDR["UserId"]);
                                if (!objSDR["FoodId"].Equals(DBNull.Value))
                                    cr.FoodId = Convert.ToInt32(objSDR["FoodId"]);
                                if (!objSDR["Count"].Equals(DBNull.Value))
                                    cr.Count = Convert.ToInt32(objSDR["Count"]);

                                cartList.Add(cr);
                            }
                        }

                        return cartList;
                        #endregion ReadData and Set Controls
                    }

                    catch (SqlException sqlex)
                    {
                        Message = sqlex.Message.ToString();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.Message.ToString();
                        return null;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }
    }
}
