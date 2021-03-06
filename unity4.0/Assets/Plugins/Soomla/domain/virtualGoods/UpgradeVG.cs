/*
 * Copyright (C) 2012 Soomla Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using UnityEngine;
using System.Collections;


namespace com.soomla.unity{	
	
	/// <summary>
	/// An upgrade virtual good is one VG in a series of VGs that define an upgrade scale of an associated VirtualGood.
	///
	/// This type of virtual good is best explained with an example:
	/// Lets say you have a strength attribute to your character in the game and that strength is on the scale 1-5.
	/// You want to provide your users with the ability to upgrade that strength. This is what you'll need to create:
	///  1. SingleUseVG for 'strength'
	///  2. UpgradeVG for strength 'level 1'
	///  3. UpgradeVG for strength 'level 2'
	///  4. UpgradeVG for strength 'level 3'
	///  5. UpgradeVG for strength 'level 4'
	///  6. UpgradeVG for strength 'level 5'
	///
	/// Now, when the user buys this UpgradeVG, we check and make sure the appropriate conditions are met and buy it for you
	/// (which actually means we upgrading the associated VirtualGood).
	///
	/// This VirtualItem is purchasable.
 	/// In case you purchase this item in Google Play or the App Store(PurchaseWithMarket), You need to define the item in Google
 	/// Play Developer Console or in iTunesConnect. (https://play.google.com/apps/publish) (https://itunesconnect.apple.com)
	/// </summary>
	public class UpgradeVG : VirtualGood{
		
//		private static string TAG = "SOOMLA UpgradeVG";
		public string GoodItemId;
		public string NextItemId;
		public string PrevItemId;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="com.soomla.unity.SingleUsePackVG"/> class.
		/// </summary>
		/// <param name='goodItemId'>
		/// The itemId of the VirtualGood associated with this upgrade.
		/// </param>
		/// <param name='nextItemId'>
		/// The itemId of the UpgradeVG after or if this is the first UpgradeVG in the scale then the value is null.
		/// </param>
		/// <param name='prevItemId'>
		/// The itemId of the UpgradeVG before or if this is the first UpgradeVG in the scale then the value is null.
		/// </param>
		/// <param name='name'>
		/// see parent
		/// </param>
		/// <param name='description'>
		/// see parent
		/// </param>
		/// <param name='itemId'>
		/// see parent
		/// </param>
		/// <param name='purchaseType'>
		/// see parent
		/// </param>
		public UpgradeVG(string goodItemId, string nextItemId, string prevItemId, string name, string description, string itemId, PurchaseType purchaseType)
			: base(name, description, itemId, purchaseType)
		{
			this.GoodItemId = goodItemId;
			this.PrevItemId = prevItemId;
			this.NextItemId = nextItemId;
		}
		
#if UNITY_ANDROID
		public UpgradeVG(AndroidJavaObject jniUpgradeVG) 
			: base(jniUpgradeVG)
		{
			GoodItemId = jniUpgradeVG.Call<string>("getGoodItemId");
			NextItemId = jniUpgradeVG.Call<string>("getNextItemId");
			PrevItemId = jniUpgradeVG.Call<string>("getPrevItemId");
		}
#endif
		/// <summary>
		/// see parent
		/// </summary>
		public UpgradeVG(JSONObject jsonItem)
			: base(jsonItem)
		{
			GoodItemId = jsonItem[JSONConsts.VGU_GOOD_ITEMID].str;
	        PrevItemId = jsonItem[JSONConsts.VGU_PREV_ITEMID].str;
			NextItemId = jsonItem[JSONConsts.VGU_NEXT_ITEMID].str;
		}

		/// <summary>
		/// see parent
		/// </summary>
		public override JSONObject toJSONObject() 
		{
	        JSONObject jsonObject = base.toJSONObject();
            jsonObject.AddField(JSONConsts.VGU_GOOD_ITEMID, this.GoodItemId);
            jsonObject.AddField(JSONConsts.VGU_PREV_ITEMID, string.IsNullOrEmpty(this.PrevItemId) ? "" : this.PrevItemId);
			jsonObject.AddField(JSONConsts.VGU_NEXT_ITEMID, string.IsNullOrEmpty(this.NextItemId) ? "" : this.NextItemId);
	
	        return jsonObject;
		}
		
	
	}
}
