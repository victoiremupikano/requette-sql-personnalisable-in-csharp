using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace reql_personnaliser
{
    internal class Program
    {
        public static void Main()
        {
            string[] fields = { "(select member.id_member from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Id_resp", "(select member.names from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Noms_resp", "(select member.kind from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Kind_resp", "(select member.birthday from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Birthday_resp", "(select member.site_birth from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Site_resp", "(select member.career from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Career_resp", "(select member.dependent from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Dependent_resp", "(select member.civil_state from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Civil_state_resp", "(select member.province from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Province_resp", "(select member.town from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Town_resp", "(select member.commune from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Commune_resp", "(select member.qr from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Qr_resp", "(select member.territory from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Territory_resp", "(select member.tel1 from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Tel1_resp", "(select member.tel2 from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Tel2_resp", "(select member.img from member where member.fk_cat_member = tf.fk_cat_member and member.dependent = 'False') as Img_resp", "(tf.id_member) as Id", "(tf.names) as Names", "(tf.kind) as Kind", "(tf.birthday) as Birthday", "(tf.site_birth) as Site", "(tf.career) as Career", "(tf.dependent) as Dependance", "(tf.civil_state) as Civil_state", "(tf.province) as Province", "(tf.town) as Town", "(tf.commune) as Commune", "(tf.qr) as Qr", "(tf.territory) as Territory", "(tf.tel1) as Tel1", "(tf.tel2) as Tel2", "(tf.img) as Img","ifnull((year(now()) - year(tf.birthday)), 0) as Age" };
            
            string[] conditions = new string[]
            {
                "date(tf.row_add) between '2023-11-03' and '2023-11-03'",
                "tf.kind = 'Féminin'",
                "tf.fk_cat_member = 'HM1-2'",
                "cat_member.fk_cat_adhesion = 'HM1-2'",
                "tf.id_member = 'HM1-3'",
                "tf.fk_zs = 'HM1-1'",
                "zs.fk_ds = 'HM1-1'",
                "(ifnull((year(now()) - year(tf.birthday)), 0)) >= '0' and (ifnull((year(now()) - year(tf.birthday)), 0)) <= '100'",
                "affect.fk_cohorte = 'HM1-1'"
            };

            string sqlQuery = BuildQuery(conditions, true, true, true, true, true, fields);
            Console.WriteLine(sqlQuery);
            Console.ReadKey();
        }

        public static string BuildQuery(string[] conditions, bool joinTable1_cat_member, bool joinTable2_cat_adhesion, bool joinTable3_zs, bool joinTable4_ds, bool joinTable5_cohorte, string[] fields)
        {
            // Construire la partie SELECT avec les champs spécifiés
            string selectClause = "SELECT ";
            if (fields != null && fields.Length > 0)
            {
                selectClause += string.Join(", ", fields);
            }
            else
            {
                selectClause += "*";
            }

            // Construire la clause FROM avec la table principale
            string query = $"{selectClause} FROM member as tf";

            // Ajouter les jointures conditionnelles
            if (joinTable1_cat_member)
            {
                query += " inner join cat_member on cat_member.id_cat_member = tf.fk_cat_member";
            }

            if (joinTable2_cat_adhesion)
            {
                query += " inner join cat_adhesion on cat_adhesion.id_cat_adhesion = cat_member.fk_cat_adhesion";
            }            
            if (joinTable3_zs)
            {
                query += " inner join zs on zs.id_zs = tf.fk_zs";
            }
            if (joinTable4_ds)
            {
                query += " inner join ds on ds.id_ds = zs.fk_ds";
            }
            if (joinTable5_cohorte)
            {
                query += " inner join affect on affect.id_affect = tf.id_member";
            }

            // Ajouter les conditions dynamiques
            if (conditions != null && conditions.Length > 0)
            {
                query += " WHERE 1=1";
                foreach (var condition in conditions)
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        query += $" AND {condition}";
                    }
                }
            }

            return query;
        }
    }
}
