namespace AcidLabsTest.Service.Frameworks.DataReadOnly
{
    public static class AmazonDynamoDbUserData
    {
        #region Table
        public static readonly string UserTable = "User";
        #endregion

        #region DataTableUser
        public static readonly string Id = "Id";
        public static readonly string Rut = "Rut";
        public static readonly string FirstName = "FirstName";
        public static readonly string LastName = "LastName";
        public static readonly string Email = "Email";
        #endregion

        #region ExpressionNameDataTable
        public static readonly string RutExpressionName = "#R";
        public static readonly string FirstNameExpressionName = "#FN";
        public static readonly string LastNameExpressionName = "#LN";
        public static readonly string EmailExpressionName = "#EM";
        #endregion

        #region ExpressionValueDataTable
        public static readonly string RutExpressionValue = ":R";
        public static readonly string FirstNameExpressionValue = ":FN";
        public static readonly string LastNameExpressionValue = ":LN";
        public static readonly string EmailExpressionValue = ":EM";
        #endregion

        #region CrudExpression
        public static readonly string UpdateExpression = "SET #R=:R, #FN=:FN, #LN=:LN";
        public static readonly string GetByEmailExpression = "#EM=:EM";
        #endregion

        #region ErrorMessages
        public static readonly string UpdateRequestNull = "Email";
        public static string EmailNotExists(string userEmail) => $"User with email {userEmail} not registered";
        public static string IdNotExists(string userId) => $"User with id {userId} not registered";
        public static string ReservedUserCannotBeDelete(string userEmail) => $"User with email {userEmail} is reserved, cannot be removed";
        #endregion

        #region ReservedUsersMail
        public static readonly string FranciscaEmailReserved = "franciscaf.diazg@gmail.com";
        #endregion
    }
}
