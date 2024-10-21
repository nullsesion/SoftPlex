using SoftPlex.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using SoftPlex.Domain.ValueObject;

namespace SoftPlex.DataAccess.Entities
{
	static public class TryToProductVersion
	{
		//todo: write log
		public static bool ProductVersionEntityToProductVersion(this ProductVersionEntity productVersionEntity, out ProductVersion productVersion)
		{
			productVersion = null;
			Result<SizeBox> sizeBox = SizeBox
				.Create(productVersionEntity.Width
					, productVersionEntity.Height
					, productVersionEntity.Length);

			if (sizeBox.IsFailure)
				return false;

			Result<ProductVersion> pve = ProductVersion.Create(
				productVersionEntity.Id
				, productVersionEntity.ProductId
				, productVersionEntity.Name
				, productVersionEntity.Description
				, sizeBox.Value
				, productVersionEntity.CreatingDate
			);

			if (pve.IsFailure)
				return false;

			productVersion = pve.Value;

			return true;
		}

		public static bool ListProductVersionEntityToListProductVersion(this List<ProductVersionEntity> productVersionEntities, out List<ProductVersion> productVersions)
		{
			productVersions = new List<ProductVersion>();
			List<ProductVersion> productVersionTmp = new List<ProductVersion>();
			foreach (ProductVersionEntity pve in productVersionEntities)
			{
				if (!pve.ProductVersionEntityToProductVersion(out ProductVersion pv))
					return false;
				else
					productVersionTmp.Add(pv);
			}
			productVersions = productVersionTmp;
			return true;
		}
	}
}
