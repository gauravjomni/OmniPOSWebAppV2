USE [V2_OmniPOS]
GO
/****** Object:  StoredProcedure [dbo].[getTaxInfo]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getTaxInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getTaxInfo]
GO
/****** Object:  StoredProcedure [dbo].[getUnitDetails]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getUnitDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getUnitDetails]
GO
/****** Object:  StoredProcedure [dbo].[getUserDetails]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getUserDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getUserDetails]
GO
/****** Object:  StoredProcedure [dbo].[getUserGroupDetails]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getUserGroupDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getUserGroupDetails]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Add_User_Attendance]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Add_User_Attendance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Add_User_Attendance]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_cate_subcate_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_cate_subcate_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_cate_subcate_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Category_Clone]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Category_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Category_Clone]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_ClearOrderTrans]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_ClearOrderTrans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_ClearOrderTrans]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Company_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Company_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Company_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Cooking_Option_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Cooking_Option_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Cooking_Option_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_CookingOption_Clone]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_CookingOption_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_CookingOption_Clone]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Course_Clone]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Course_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Course_Clone]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Course_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Course_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Course_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Customer_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Customer_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Customer_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_DeviceInfo_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_DeviceInfo_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_DeviceInfo_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_GroupPermission_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_GroupPermission_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_GroupPermission_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Ingredient_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Ingredient_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Ingredient_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Kitchen_Clone]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Kitchen_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Kitchen_Clone]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Kitchen_Instruction]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Kitchen_Instruction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Kitchen_Instruction]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_kitchen_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_kitchen_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_kitchen_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_LastUpdates]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_LastUpdates]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_LastUpdates]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_modifer_level_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_modifer_level_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_modifer_level_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Modifier_Clone]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Modifier_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Modifier_Clone]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Modifier_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Modifier_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Modifier_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_ModifierLevel_Clone]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_ModifierLevel_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_ModifierLevel_Clone]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_NoSale_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_NoSale_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_NoSale_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Option_Settings_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Option_Settings_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Option_Settings_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Order_Adjustment_Log_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Order_Adjustment_Log_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Order_Adjustment_Log_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Order_Adjustment_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Order_Adjustment_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Order_Adjustment_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Order_PaymentInfo_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Order_PaymentInfo_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Order_PaymentInfo_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Order_Product_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Order_Product_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Order_Product_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Order_ProductInfo_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Order_ProductInfo_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Order_ProductInfo_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_OrderInfo_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_OrderInfo_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_OrderInfo_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_OrderNote]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_OrderNote]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_OrderNote]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_OrderTransaction_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_OrderTransaction_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_OrderTransaction_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Payout_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Payout_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Payout_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Printer_Clone]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Printer_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Printer_Clone]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Printer_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Printer_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Printer_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Clone]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Product_Clone]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Cooking_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Cooking_Options_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Product_Cooking_Options_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Kitchen_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Kitchen_Options_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Product_Kitchen_Options_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Kitchen_Printer_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Kitchen_Printer_Options_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Product_Kitchen_Printer_Options_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Mixing_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Mixing_Options_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Product_Mixing_Options_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Options_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Product_Options_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Printer_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Printer_Options_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Product_Printer_Options_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Product_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Refund_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Refund_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Refund_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Restaurant_Info_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Restaurant_Info_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Restaurant_Info_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Restaurant_Settings_Info_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Restaurant_Settings_Info_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Restaurant_Settings_Info_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_State_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_State_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_State_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_SubCategory_Clone]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_SubCategory_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_SubCategory_Clone]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_SubRecipe_Mixing_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_SubRecipe_Mixing_Options_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_SubRecipe_Mixing_Options_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_SubRecipe_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_SubRecipe_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_SubRecipe_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Supplier_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Supplier_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_Supplier_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_TaxInfo_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_TaxInfo_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_TaxInfo_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_UnitMaster_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_UnitMaster_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_UnitMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_user_group_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_user_group_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_user_group_Update]
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_user_Update]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_user_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_omni_user_Update]
GO
/****** Object:  StoredProcedure [dbo].[getCustomerDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getCustomerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getCustomerDetails]
GO
/****** Object:  StoredProcedure [dbo].[getDeviceInfo]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getDeviceInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getDeviceInfo]
GO
/****** Object:  UserDefinedFunction [dbo].[GetIngredient_SubRcpUnit]    Script Date: 03/08/2014 23:00:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetIngredient_SubRcpUnit]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetIngredient_SubRcpUnit]
GO
/****** Object:  StoredProcedure [dbo].[getIngredientDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getIngredientDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getIngredientDetails]
GO
/****** Object:  StoredProcedure [dbo].[getKitchen_Instruction]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getKitchen_Instruction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getKitchen_Instruction]
GO
/****** Object:  StoredProcedure [dbo].[getKitchenDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getKitchenDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getKitchenDetails]
GO
/****** Object:  StoredProcedure [dbo].[getModifierDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getModifierDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getModifierDetails]
GO
/****** Object:  StoredProcedure [dbo].[getModifierLevelDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getModifierLevelDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getModifierLevelDetails]
GO
/****** Object:  StoredProcedure [dbo].[getOption_Settings]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getOption_Settings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getOption_Settings]
GO
/****** Object:  StoredProcedure [dbo].[getOrder_Note]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getOrder_Note]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getOrder_Note]
GO
/****** Object:  UserDefinedFunction [dbo].[getOrderIDSWithinDateRangeNew]    Script Date: 03/08/2014 23:00:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getOrderIDSWithinDateRangeNew]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[getOrderIDSWithinDateRangeNew]
GO
/****** Object:  UserDefinedFunction [dbo].[GetOrderPaymentModeStatus]    Script Date: 03/08/2014 23:00:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrderPaymentModeStatus]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetOrderPaymentModeStatus]
GO
/****** Object:  StoredProcedure [dbo].[getPrinterDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getPrinterDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getPrinterDetails]
GO
/****** Object:  StoredProcedure [dbo].[getProduct_Kitchen_Option]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProduct_Kitchen_Option]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getProduct_Kitchen_Option]
GO
/****** Object:  StoredProcedure [dbo].[getProduct_Kitchen_Printer_Option]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProduct_Kitchen_Printer_Option]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getProduct_Kitchen_Printer_Option]
GO
/****** Object:  StoredProcedure [dbo].[getProduct_Mixing_Options]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProduct_Mixing_Options]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getProduct_Mixing_Options]
GO
/****** Object:  StoredProcedure [dbo].[getProduct_Printer_Option]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProduct_Printer_Option]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getProduct_Printer_Option]
GO
/****** Object:  StoredProcedure [dbo].[getProductCookingOptions]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProductCookingOptions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getProductCookingOptions]
GO
/****** Object:  StoredProcedure [dbo].[getProductDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProductDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getProductDetails]
GO
/****** Object:  StoredProcedure [dbo].[getProductModifiers]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProductModifiers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getProductModifiers]
GO
/****** Object:  StoredProcedure [dbo].[getRestaurant_Settings_Detail]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getRestaurant_Settings_Detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getRestaurant_Settings_Detail]
GO
/****** Object:  StoredProcedure [dbo].[getRestaurantDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getRestaurantDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getRestaurantDetails]
GO
/****** Object:  StoredProcedure [dbo].[getStateDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getStateDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getStateDetails]
GO
/****** Object:  StoredProcedure [dbo].[getSubRecipe_Mixing_Options]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getSubRecipe_Mixing_Options]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getSubRecipe_Mixing_Options]
GO
/****** Object:  StoredProcedure [dbo].[getSubRecipeDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getSubRecipeDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getSubRecipeDetails]
GO
/****** Object:  StoredProcedure [dbo].[getSupplierDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getSupplierDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getSupplierDetails]
GO
/****** Object:  StoredProcedure [dbo].[getCategoryDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getCategoryDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getCategoryDetails]
GO
/****** Object:  StoredProcedure [dbo].[getCompanyDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getCompanyDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getCompanyDetails]
GO
/****** Object:  StoredProcedure [dbo].[getCookingOptionDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getCookingOptionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getCookingOptionDetails]
GO
/****** Object:  StoredProcedure [dbo].[getCourseDetails]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getCourseDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getCourseDetails]
GO
/****** Object:  StoredProcedure [dbo].[getCurrentDate]    Script Date: 03/08/2014 22:59:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getCurrentDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getCurrentDate]
GO
/****** Object:  StoredProcedure [dbo].[getTableColumn]    Script Date: 03/08/2014 22:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getTableColumn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getTableColumn]
GO
/****** Object:  Table [dbo].[omni_users]    Script Date: 03/08/2014 22:59:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_users](
	[UserId] [int] NOT NULL,
	[FirstName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserAlias] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserName] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserPin] [int] NULL,
	[UserEmail] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserPassword] [binary](50) NULL,
	[UserPhone] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StartDate] [datetime] NULL,
	[UserGroupID] [int] NULL,
	[Rest_id] [int] NULL,
	[IsActive] [tinyint] NULL,
	[CreateDate] [datetime] NULL,
	[CreatedByUserID] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[DeletedByUserID] [int] NULL,
	[DeleteDate] [datetime] NULL,
	[HourlyRate] [decimal](7, 2) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
If Not exists(Select 1 from omni_users where UserId = 3)
INSERT [dbo].[omni_users] ([UserId], [FirstName], [LastName], [UserAlias], [UserName], [UserPin], [UserEmail], [UserPassword], [UserPhone], [StartDate], [UserGroupID], [Rest_id], [IsActive], [CreateDate], [CreatedByUserID], [ModifyDate], [ModifiedByUserID], [DeletedByUserID], [DeleteDate], [HourlyRate]) VALUES (3, N'Admin', N'Admin', N'admin', N'admin', 1234, N'admin@omniposweb.com', 0x3078453332383944444244334644363336313330443044383843413541384437444230303030303030303030303030303030, N'xxxxxxxxx', CAST(0x0000A26900000000 AS DateTime), 1, 1, 1, NULL, NULL, CAST(0x0000A2DC011CC2E3 AS DateTime), 3, NULL, NULL, CAST(0.00 AS Decimal(7, 2)))
/****** Object:  Table [dbo].[omni_UserAttendance]    Script Date: 03/08/2014 22:59:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_UserAttendance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_UserAttendance](
	[TransID] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Date] [smalldatetime] NULL,
	[RestID] [int] NULL,
	[isClosed] [bit] NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[UserID] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_user_group]    Script Date: 03/08/2014 22:59:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_user_group]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_user_group](
	[UserGroupID] [int] NOT NULL,
	[UserGroupName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Location_id] [int] NULL,
	[CreateDate] [datetime] NULL,
	[CreatedByUserID] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[DeleteDate] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[Isactive] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
If Not exists(select 1 from omni_user_group where UserGroupID = 1)
INSERT [dbo].[omni_user_group] ([UserGroupID], [UserGroupName], [Location_id], [CreateDate], [CreatedByUserID], [ModifyDate], [ModifiedByUserID], [DeleteDate], [DeletedByUserID], [Isactive]) VALUES (1, N'Administrator', 0, CAST(0x0000A15400000000 AS DateTime), 3, CAST(0x0000A15400000000 AS DateTime), 3, NULL, NULL, 1)
If Not exists(select 1 from omni_user_group where UserGroupID = 35)
INSERT [dbo].[omni_user_group] ([UserGroupID], [UserGroupName], [Location_id], [CreateDate], [CreatedByUserID], [ModifyDate], [ModifiedByUserID], [DeleteDate], [DeletedByUserID], [Isactive]) VALUES (35, N'Manager', NULL, CAST(0x0000A162015DA799 AS DateTime), 3, NULL, NULL, NULL, NULL, 1)
If Not exists(select 1 from omni_user_group where UserGroupID = 36)
INSERT [dbo].[omni_user_group] ([UserGroupID], [UserGroupName], [Location_id], [CreateDate], [CreatedByUserID], [ModifyDate], [ModifiedByUserID], [DeleteDate], [DeletedByUserID], [Isactive]) VALUES (36, N'Waiter', NULL, CAST(0x0000A1700159A82D AS DateTime), 3, NULL, NULL, NULL, NULL, 1)
If Not exists(select 1 from omni_user_group where UserGroupID = 37)
INSERT [dbo].[omni_user_group] ([UserGroupID], [UserGroupName], [Location_id], [CreateDate], [CreatedByUserID], [ModifyDate], [ModifiedByUserID], [DeleteDate], [DeletedByUserID], [Isactive]) VALUES (37, N'Quick Service', NULL, CAST(0x0000A1B300008954 AS DateTime), 3, NULL, NULL, NULL, NULL, 1)
/****** Object:  Table [dbo].[omni_UnitMaster]    Script Date: 03/08/2014 22:59:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_UnitMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_UnitMaster](
	[UnitID] [int] NOT NULL,
	[UnitName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Rest_ID] [int] NULL,
	[IsActive] [tinyint] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[DeletedOn] [datetime] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_TransactionLog]    Script Date: 03/08/2014 22:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_TransactionLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_TransactionLog](
	[Description] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TransactionDate] [datetime] NULL,
	[Rest_ID] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Tran_ItemHistory]    Script Date: 03/08/2014 22:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Tran_ItemHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Tran_ItemHistory](
	[ItemHisNo] [int] NOT NULL,
	[Tran_Code] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Tran_No] [int] NOT NULL,
	[TranD_No] [int] NULL,
	[Tran_Type] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Tran_Desc] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Tran_Date] [datetime] NOT NULL,
	[ProductID] [int] NULL,
	[ProductType] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_ID] [int] NOT NULL,
	[Qty] [decimal](8, 2) NULL,
	[Acc_Year] [int] NOT NULL,
	[Acc_Month] [int] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_TimeZone]    Script Date: 03/08/2014 22:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_TimeZone]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_TimeZone](
	[ID] [int] NOT NULL,
	[TimeZoneID] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TimeZoneDisplayName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_TaxInfo]    Script Date: 03/08/2014 22:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_TaxInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_TaxInfo](
	[TaxInfoID] [int] NOT NULL,
	[TaxInfoLiteral] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TaxRate] [decimal](6, 2) NULL,
	[Rest_ID] [int] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[DeletedOn] [datetime] NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Suppliers]    Script Date: 03/08/2014 22:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Suppliers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Suppliers](
	[SupplierId] [int] NOT NULL,
	[Email] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FirstName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address1] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address2] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StateName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ZipCode] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Phone] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShipTo] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShippingName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShipAddress1] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShipAddress2] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShippingCity] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShippingState] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShippingZip] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShippingPhone] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FOB] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShippingTerms] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Requistioner] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Tax] [decimal](9, 2) NULL,
	[SpecialInstructions] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NameForAuthorized] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VendorCompany] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactEmail] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactPhone] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactFax] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TaxRate] [decimal](9, 2) NULL,
	[SHAmount] [decimal](9, 2) NULL,
	[MiscAmount] [decimal](9, 2) NULL,
	[IngredientId] [int] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[Rest_id] [int] NULL,
	[IsActive] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_SubRecipe_Mixing_Details]    Script Date: 03/08/2014 22:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_SubRecipe_Mixing_Details]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_SubRecipe_Mixing_Details](
	[DetailID] [int] NOT NULL,
	[SubRecipeID] [int] NULL,
	[IngredientID] [int] NULL,
	[Qty] [decimal](6, 2) NULL,
	[Rest_ID] [int] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[omni_SubRecipe]    Script Date: 03/08/2014 22:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_SubRecipe]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_SubRecipe](
	[SubRecipeID] [int] NOT NULL,
	[SubRecipeName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubRecipeDesc] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UnitID] [int] NULL,
	[Rest_ID] [int] NULL,
	[IsActive] [tinyint] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedByUserID] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_State]    Script Date: 03/08/2014 22:59:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_State]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_State](
	[StateID] [int] NOT NULL,
	[StateName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Restuarnt_info_Settings]    Script Date: 03/08/2014 22:59:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Restuarnt_info_Settings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Restuarnt_info_Settings](
	[Opt_SettingsID] [int] NOT NULL,
	[CustomerView] [bit] NULL,
	[AddLines_Between_Order_Item] [bit] NULL,
	[Sort_Course_By] [tinyint] NULL,
	[Print_Transferred_Order] [bit] NULL,
	[Print_Deleted_Order] [bit] NULL,
	[Print_Voided_Items] [bit] NULL,
	[Kitchen_View_Timeout] [smallint] NULL,
	[Allow_Void_Order_Item] [bit] NULL,
	[Allow_Delete_Send_Order] [bit] NULL,
	[Quick_Service] [smallint] NULL,
	[Table_Layout_Type] [smallint] NULL,
	[Auto_Prompt_Tip] [bit] NULL,
	[Sort_Items_By] [smallint] NULL,
	[Sort_SubCategories_By] [smallint] NULL,
	[Sort_Products_By] [smallint] NULL,
	[No_Of_Devices] [smallint] NULL,
	[Use_Table_Layout] [bit] NULL,
	[Rest_ID] [int] NOT NULL,
	[shouldEmailZReport] [bit] NULL,
	[shouldNotifyNoSale] [bit] NULL,
	[ShouldEnableClockIn] [bit] NULL,
	[ScannerMode] [smallint] NULL,
	[HoldAndFire] [smallint] NULL,
	[CashDrawerBalancing] [bit] NULL,
	[NoSaleLimit] [int] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[omni_Restuarnt_info]    Script Date: 03/08/2014 22:59:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Restuarnt_info]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Restuarnt_info](
	[Rest_Id] [int] NOT NULL,
	[RestName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Initials] [nchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address1] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address2] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StateID] [int] NULL,
	[Zip] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Phone] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fax] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Website] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TablesCount] [int] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[Header_Name] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Header_Address1] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Header_City] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Header_State] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Header_Zip] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Header_Phone] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Header_ABN] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Header_TaxInvoice] [bit] NULL,
	[Header_Website] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Header_Email] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Footer1] [varchar](5000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Footer2] [varchar](5000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[KitchenView] [bit] NULL,
	[ExpediteView] [bit] NULL,
	[Tax] [bit] NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Refund]    Script Date: 03/08/2014 22:59:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Refund]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Refund](
	[TransactionID] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RefundDate] [datetime] NULL,
	[ProductID] [int] NULL,
	[DeviceID] [int] NULL,
	[Comments] [varchar](5000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_ID] [int] NULL,
	[PaymentTypeID] [int] NULL,
	[Amount] [decimal](10, 2) NULL,
	[CustFName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CustLName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[DeletedOn] [datetime] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_PurchaseMaster]    Script Date: 03/08/2014 22:59:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_PurchaseMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_PurchaseMaster](
	[POID] [int] NOT NULL,
	[PONo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PODate] [datetime] NULL,
	[SupplierID] [tinyint] NULL,
	[TotalAmt] [decimal](14, 2) NULL,
	[TaxAmt] [decimal](8, 2) NULL,
	[DeliverToName] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DeliverAddress1] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DeliverAddress2] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OrderNote] [varchar](5000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsInvoiceGenerated] [bit] NULL,
	[IsActive] [tinyint] NULL,
	[Rest_ID] [int] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[DeletedOn] [datetime] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_PurchaseDetail]    Script Date: 03/08/2014 22:59:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_PurchaseDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_PurchaseDetail](
	[POID] [int] NULL,
	[ProductID] [int] NULL,
	[ProductType] [char](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Qty] [decimal](9, 4) NULL,
	[UnitPrice] [decimal](12, 4) NULL,
	[Rest_ID] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Products]    Script Date: 03/08/2014 22:59:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Products]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Products](
	[ProductID] [int] NOT NULL,
	[ProductName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProductName2] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductDescription] [varchar](3000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Color] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GST] [bit] NULL,
	[HasOpenPrice] [bit] NULL,
	[HasFlag] [bit] NULL,
	[ChangePrice] [bit] NULL,
	[IsSoleProduct] [bit] NULL,
	[IsDailyItem] [bit] NULL,
	[Price1] [decimal](7, 2) NULL,
	[Price2] [decimal](7, 2) NULL,
	[Price3] [decimal](7, 2) NULL,
	[Price4] [decimal](7, 2) NULL,
	[Price5] [decimal](7, 2) NULL,
	[ReOrderQty] [decimal](8, 2) NULL,
	[ReOrderLevel] [decimal](8, 2) NULL,
	[BarCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OpQty] [decimal](8, 2) NULL,
	[StockInHand] [int] NULL,
	[SortOrder] [tinyint] NULL,
	[CategoryID] [int] NULL,
	[ProductImageWithPath] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CourseID] [int] NULL,
	[SupplierID] [int] NULL,
	[UnitID] [int] NULL,
	[Rest_ID] [int] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[DeletedOn] [datetime] NULL,
	[IsActive] [tinyint] NULL,
	[Points] [decimal](7, 2) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Product_Modifiers]    Script Date: 03/08/2014 22:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Product_Modifiers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Product_Modifiers](
	[Product_ModifierId] [int] NOT NULL,
	[ModifierID] [int] NULL,
	[ProductID] [int] NULL,
	[Rest_ID] [int] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[omni_Product_Kitchen_Printer_Options]    Script Date: 03/08/2014 22:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Product_Kitchen_Printer_Options]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Product_Kitchen_Printer_Options](
	[Product_Ktch_PrntID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[OptionID] [int] NULL,
	[OptionType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_ID] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Product_Ingredient_SubreciepeDetails]    Script Date: 03/08/2014 22:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Product_Ingredient_SubreciepeDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Product_Ingredient_SubreciepeDetails](
	[ID] [int] NOT NULL,
	[ProductId] [int] NULL,
	[IngredientId] [int] NULL,
	[MixingType] [char](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_Id] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Product_Cooking_Options]    Script Date: 03/08/2014 22:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Product_Cooking_Options]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Product_Cooking_Options](
	[Product_CookingOptionID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[OptionID] [int] NULL,
	[Rest_ID] [int] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[omni_Printers]    Script Date: 03/08/2014 22:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Printers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Printers](
	[PrinterId] [int] NOT NULL,
	[PrinterName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IPAddress] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_id] [int] NULL,
	[PosorItem] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PrinterType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NoOfCopies] [int] NULL,
	[Trigger_Cash_Drawer] [tinyint] NULL,
	[IsPrintIpAddress] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[isDeleted] [bit] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[DeletedOn] [datetime] NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Payout]    Script Date: 03/08/2014 22:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Payout]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Payout](
	[TransactionID] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TransactionDate] [datetime] NULL,
	[DeviceID] [int] NULL,
	[Comments] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_ID] [int] NULL,
	[PaymentTypeID] [int] NULL,
	[Amount] [decimal](10, 2) NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeltedByUserID] [int] NULL,
	[DeletedOn] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_PaymentType]    Script Date: 03/08/2014 22:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_PaymentType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_PaymentType](
	[PaymentTypeID] [int] NOT NULL,
	[PaymentType] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [tinyint] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[DeletedOn] [datetime] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_OrderPaymentInfo]    Script Date: 03/08/2014 22:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_OrderPaymentInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_OrderPaymentInfo](
	[OrderPaymentId] [int] NOT NULL,
	[Order_TranID] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PaymentOn] [datetime] NULL,
	[Credit] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CCNo] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TransactionID] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FirstName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PaymentTypeID] [int] NULL,
	[ChequeNo] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ChequeAmount] [decimal](18, 2) NULL,
	[PaidAmount] [decimal](18, 2) NULL,
	[Comments] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_OrderInfo]    Script Date: 03/08/2014 22:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_OrderInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_OrderInfo](
	[Order_TranID] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TransactionDate] [datetime] NULL,
	[OrderNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OrderedOn] [datetime] NULL,
	[TableId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NoOfGuest] [int] NULL,
	[Table_OpenedOn] [datetime] NULL,
	[Table_ClosedOn] [datetime] NULL,
	[SessionId] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_id] [int] NULL,
	[Comment] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserID] [int] NULL,
	[DeviceID] [int] NULL,
	[GrossAmount] [decimal](18, 2) NULL,
	[TotalTax] [decimal](8, 2) NULL,
	[TipAmount] [decimal](8, 2) NULL,
	[Discount] [decimal](8, 2) NULL,
	[Surcharge] [decimal](8, 2) NULL,
	[TotalAmount] [decimal](18, 2) NULL,
	[AdjustmentPercent] [decimal](6, 2) NULL,
	[AmountPaid] [decimal](18, 2) NULL,
	[PaymentType] [tinyint] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[isPaid] [bit] NULL,
	[isPartial] [bit] NULL,
	[CustomerID] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Order_ProductDetails_Twin]    Script Date: 03/08/2014 22:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Order_ProductDetails_Twin]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Order_ProductDetails_Twin](
	[Order_TranID] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProductID] [int] NULL,
	[Qty] [decimal](5, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[ModifierIDS] [varbinary](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Order_ProductDetails]    Script Date: 03/08/2014 22:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Order_ProductDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Order_ProductDetails](
	[Order_TranID] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProductID] [int] NULL,
	[Qty] [decimal](5, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[ModifierIDS] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Order_Note]    Script Date: 03/08/2014 22:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Order_Note]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Order_Note](
	[NoteID] [int] NOT NULL,
	[Message] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_ID] [int] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[omni_Order_Adjustment_Log]    Script Date: 03/08/2014 22:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Order_Adjustment_Log]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Order_Adjustment_Log](
	[ID] [int] NOT NULL,
	[FromDate] [datetime] NULL,
	[TillDate] [datetime] NULL,
	[OrderTranID] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_ID] [int] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[omni_Option_Settings]    Script Date: 03/08/2014 22:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Option_Settings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Option_Settings](
	[OptionID] [int] NOT NULL,
	[OptionName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OptionValue] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_NoSale]    Script Date: 03/08/2014 22:59:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_NoSale]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_NoSale](
	[TransID] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Date] [smalldatetime] NULL,
	[RestID] [int] NULL,
	[NoSaleCount] [int] NULL,
	[DeviceID] [int] NULL,
	[UserID] [int] NULL,
	[Note] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Modules]    Script Date: 03/08/2014 22:59:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Modules]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Modules](
	[MenuID] [int] NOT NULL,
	[MenuName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParentMenuID] [int] NULL,
	[MainMenuOrder] [int] NULL,
	[SubMenuOrder] [int] NULL,
	[NavigateUrl] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MenuValue] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MainMenuName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MenuOrder] [int] NULL,
	[ImageUrl] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [tinyint] NULL,
	[IsLocationBased] [tinyint] NULL,
	[IsViewInMenuNavigation] [tinyint] NULL,
	[IsMenuShownWithinLocationLevel] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
--select 'if not exists (select 1 from omni_Modules where MenuID = '+convert(varchar(20),MenuId)+' ) ',
--'INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES 
--('+convert(varchar(20),MenuID)+','''+ MenuName+''','+ convert(varchar(20),ParentMenuID)+','+  convert(varchar(20),MainMenuOrder)+','+  convert(varchar(20),isnull(SubMenuOrder,''))+','''+  isnull(NavigateUrl,'')+''','''+  isnull(MenuValue,'')+''','''','+ convert(varchar(20),isnull(MenuOrder,0))+','''','+  convert(varchar(20),IsActive)+','+  convert(varchar(20),IsLocationBased)+','+  convert(varchar(20),IsViewInMenuNavigation)+','+  convert(varchar(20),IsMenuShownWithinLocationLevel)+')'
--from [omni_Modules]
if not exists (select 1 from omni_Modules where MenuID = 71 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (71,'Sales Summary',4,3,4,'sales_summary.aspx','Sales Summary','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 72 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (72,'Unit Master',78,1,1,'UnitMaster.aspx','Unit Master','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 73 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (73,'Add Unit',78,2,2,'AddUnit.aspx','Add Unit','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 74 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (74,'Ingredient Master',78,3,3,'Ingredients.aspx','Ingredient Master','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 75 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (75,'Add Ingredient',78,4,4,'AddIngredient.aspx','Add Ingredient','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 76 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (76,'Sub Recipes',78,5,5,'SubRecipes.aspx','Sub Recipes','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 77 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (77,'Add Sub Recipe',78,6,6,'AddSubRecipe.aspx','Add Sub Recipe','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 78 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (78,'Inventory',0,5,5,'','','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 79 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (79,'Purchase Orders',78,7,7,'PurchaseOrders.aspx','Purchase Orders','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 80 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (80,'Create Purchase Order',78,8,8,'CreatePurchaseOrder.aspx','Create Purchase Order','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 81 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (81,'Good Receive Notes',78,9,3,'GoodsReceivedNotes.aspx','Good Receive Notes','',0,'',1,1,1,1)
if not exists (select 1 from omni_Modules where MenuID = 82 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (82,'Add Goods Received Note',78,10,4,'AddGoodsReceiveNote.aspx','Add Goods Received Note','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 83 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (83,'Reports [Inventory]',0,7,7,'','','',0,'',1,1,1,1)
if not exists (select 1 from omni_Modules where MenuID = 84 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (84,'Purchase Register',83,1,1,'ViewPurchaseRegister.aspx','Purchase Register','',0,'',1,1,1,1)
if not exists (select 1 from omni_Modules where MenuID = 85 ) 
	INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (85,'View ItemStock',83,2,2,'ViewItemStock.aspx','View Item Stock','',0,'',1,1,1,1)
if not exists (select 1 from omni_Modules where MenuID = 1 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (1,'Locations',0,1,0,'','Location','',1,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 2 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (2,'User Groups | Users',0,2,0,'','User Group | Users','',2,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 3 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (3,'Management (Others)',0,4,0,'','Management','',3,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 4 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (4,'Reports',0,6,0,'','Reports','',4,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 5 ) 
	INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (5,'Settings',0,10,0,'','Settings','',5,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 6 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (6,'Users',2,1,3,'Users.aspx','User','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 7 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (7,'User Groups',2,3,1,'UserGroups.aspx','User Groups','',0,'',1,0,1,1)
if not exists (select 1 from omni_Modules where MenuID = 8 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (8,'Group Permissions',2,5,2,'GroupPermissions.aspx','Group Permissions','',0,'',1,0,1,1)
if not exists (select 1 from omni_Modules where MenuID = 9 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (9,'Create User',2,2,2,'AddUser.aspx','Add User','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 10 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (10,'Create Group',2,4,4,'AddGroup.aspx','Add Group','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 11 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (11,'View Locations',1,1,1,'Restaurants.aspx','Locations','',0,'',1,0,1,1)
if not exists (select 1 from omni_Modules where MenuID = 12 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (12,'Categories / Subcategories',3,5,5,'Categories.aspx','Manage Category / SubCategory','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 13 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (13,'Create Category',3,6,1,'CreateCategory.aspx','Create Category','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 14 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (14,'Create Subcategory',3,7,1,'CreateSubCategory.aspx','Create SubCategory','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 15 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (15,'Printers',32,4,1,'Printers.aspx','Manage Printers','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 16 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (16,'Add Printer',32,5,5,'AddPrinter.aspx','Add Printer','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 17 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (17,'Kitchens',32,6,2,'Kitchens.aspx','Manage Kitchens','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 18 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (18,'Add Kitchen',32,7,7,'AddKitchen.aspx','Add Kitchen','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 19 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (19,'Products',3,26,6,'Products.aspx','Manage Products','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 20 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (20,'Add Product',3,9,9,'AddProduct.aspx','Add Product','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 21 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (21,'Product Modifier Levels',3,1,3,'Modifierlevel.aspx','Modifier Level','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 22 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (22,'Add Modifier Level',3,2,11,'AddModifierLevel.aspx','Add Modifier Level','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 23 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (23,'Product Modifiers',3,3,4,'Modifiers.aspx','Modifiers','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 24 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (24,'Add Product Modifier',3,4,13,'AddModifier.aspx','Add Modifier','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 25 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (25,'Select Location',1,14,14,'Select_Restaurants.aspx','Choose Location','',0,'',1,0,1,1)
if not exists (select 1 from omni_Modules where MenuID = 26 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (26,'Dashboard',1,15,15,'Home.aspx','DashBoard','',0,'',1,0,1,1)
if not exists (select 1 from omni_Modules where MenuID = 27 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (27,'Profile',53,1,1,'Company.aspx','Company Profile','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 28 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (28,'Total Sale – Current',4,1,1,'xreport.aspx','X-Report','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 29 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (29,'Add Location',1,6,6,'AddRestaurant.aspx','Add Location','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 30 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (30,'Add State',53,2,9,'AddState.aspx','Add State','',0,'',1,0,0,0)
if not exists (select 1 from omni_Modules where MenuID = 31 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (31,'States',1,10,10,'States.aspx','States','',0,'',0,0,0,0)
if not exists (select 1 from omni_Modules where MenuID = 32 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (32,'Management (General)',0,3,4,'','','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 33 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (33,'Special Instructions',32,1,3,'Instruction.aspx','Special Instruction','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 34 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (34,'Order Notes',32,3,4,'Notes.aspx','Order Note','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 35 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (35,'Courses',32,5,5,'Courses.aspx','Courses','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 36 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (36,'Add Course',32,6,6,'AddCourse.aspx','Add Course','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 37 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (37,'Add Instruction',32,2,7,'AddInstruction.aspx','Add Instruction','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 38 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (38,'Add Note',32,4,8,'AddNote.aspx','Add Note','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 39 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (39,'Cooking Options',32,8,9,'Options.aspx','Cooking Options','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 40 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (40,'Add Cooking Option',32,9,10,'AddCookingOption.aspx','Add Cooking Option','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 41 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (41,'Tax Rates',32,10,11,'TaxInfo.aspx','Tax Info','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 42 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (42,'Add Tax Rate',32,11,12,'AddTaxInfo.aspx','Add TaxInfo','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 43 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (43,'POS Devices',32,12,13,'PosDevices.aspx','Pos Device','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 44 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (44,'Add POS Device',32,13,10,'AddPosDevice.aspx','Add Pos Device','',0,'',1,1,0,0)
if not exists (select 1 from omni_Modules where MenuID = 45 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (45,'Hourly Sales',4,10,10,'HourlySaleReport.aspx','Hourly Sale','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 47 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (47,'Sales Comparison',53,11,11,'LocationComparisonReport.aspx','Sales Comparison','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 48 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (48,'Live Transactions',4,4,3,'ViewOrderTransaction.aspx','View Order Transaction','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 52 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (52,'Payouts and Refunds',4,5,4,'ViewPayoutRefund.aspx','View Payout / Refund','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 53 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (53,'Company Area',0,10,10,'','Company Level','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 54 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (54,'States',53,2,1,'States.aspx','View States','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 55 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (55,'Sales History (Chart)',4,9,9,'SalesChartReport.aspx','Z Report','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 56 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (56,'Tools',0,8,7,'','','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 57 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (57,'Clone Location',5,1,1,'CloneLocation.aspx','Clone Location','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 58 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (58,'Live Transactions',53,6,3,'ViewOrderTransaction_Company.aspx','View Order Transaction','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 59 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (59,'Payouts and Refunds',53,7,4,'ViewPayoutRefund_Company.aspx','View Payout / Refund','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 60 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (60,'Sales History',4,3,3,'periodic_zreport.aspx','Periodic Z-Report','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 61 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (61,'Sales History',53,8,4,'Periodic_Zreport_Company.aspx','Periodic Z-Report','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 62 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (62,'Individual Items Sold',4,6,6,'ViewItemSold.aspx','View Item Sold','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 63 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (63,'Individual Items Sold',53,9,7,'View_CompanyItemSold.aspx','View Item Sold','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 64 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (64,'Customers',53,3,8,'ViewCustomer.aspx','View Customer','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 65 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (65,'Add Customer',53,10,9,'AddCustomer.aspx','Add Customer','',0,'',1,0,0,0)
if not exists (select 1 from omni_Modules where MenuID = 66 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (66,'Suppliers',53,4,10,'ViewSupplier.aspx','View Supplier','',0,'',1,0,1,0)
if not exists (select 1 from omni_Modules where MenuID = 67 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (67,'Add Supplier',53,11,11,'AddSupplier.aspx','Add Supplier','',0,'',1,0,0,0)
if not exists (select 1 from omni_Modules where MenuID = 68 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (68,'User Attendance',4,11,11,'UserAttendancePage.aspx','User Attendance','',0,'',1,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 69 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (69,'Order Adjustment',68,1,1,'OrderAdjust.aspx','Order Adjustment','',0,'',0,1,1,0)
if not exists (select 1 from omni_Modules where MenuID = 70 ) 	
INSERT INTO [dbo].[omni_Modules] ([MenuID] ,[MenuName] ,[ParentMenuID] ,[MainMenuOrder] ,[SubMenuOrder] ,[NavigateUrl] ,[MenuValue] ,[MainMenuName] ,[MenuOrder] ,[ImageUrl] ,[IsActive] ,[IsLocationBased] ,[IsViewInMenuNavigation] ,[IsMenuShownWithinLocationLevel])     VALUES   (70,'Clear Order Transactions',56,2,2,'ClearOrderTransact.aspx','Clear Order Transactions','',0,'',1,1,1,0)

/****** Object:  Table [dbo].[omni_Modifiers_Level]    Script Date: 03/08/2014 22:59:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Modifiers_Level]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Modifiers_Level](
	[LevelID] [int] NOT NULL,
	[ModifierLevelName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_ID] [int] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Modifiers]    Script Date: 03/08/2014 22:59:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Modifiers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Modifiers](
	[ModifierId] [int] NOT NULL,
	[ModifierName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Name2] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Price1] [decimal](9, 2) NULL,
	[Price2] [decimal](9, 2) NULL,
	[Description] [varchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifierLevelID] [int] NULL,
	[SortOrder] [int] NULL,
	[Rest_ID] [int] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[GST] [bit] NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Last_Updates]    Script Date: 03/08/2014 22:59:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Last_Updates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Last_Updates](
	[ActionName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_ID] [int] NULL,
	[TransactDate] [datetime] NULL,
	[Field1] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Kitchen_Instruction]    Script Date: 03/08/2014 22:59:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Kitchen_Instruction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Kitchen_Instruction](
	[InstructionID] [int] NOT NULL,
	[Message] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_ID] [int] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[omni_Kitchen]    Script Date: 03/08/2014 22:59:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Kitchen]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Kitchen](
	[KitchenId] [int] NOT NULL,
	[KitchenName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_ID] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[SortOrder] [int] NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Items_Ingredients]    Script Date: 03/08/2014 22:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Items_Ingredients]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Items_Ingredients](
	[IngredientId] [int] NOT NULL,
	[IngredientName] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Price] [decimal](9, 2) NULL,
	[UnitId] [int] NULL,
	[SortOrder] [int] NULL,
	[Rest_ID] [int] NULL,
	[OpQty] [decimal](8, 2) NULL,
	[ReOrderQty] [decimal](8, 2) NULL,
	[ReOrderLevel] [decimal](8, 2) NULL,
	[BarCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SupplierID] [int] NULL,
	[IsDailyItem] [bit] NULL,
	[isDeleted] [bit] NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Item_Categories]    Script Date: 03/08/2014 22:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Item_Categories]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Item_Categories](
	[CategoryId] [int] NOT NULL,
	[CategoryName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Name2] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParentId] [int] NULL,
	[Rest_id] [int] NULL,
	[CreatedByUserID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[SortOrder] [int] NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Item_Balance]    Script Date: 03/08/2014 22:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Item_Balance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Item_Balance](
	[ITMBALNO] [int] NOT NULL,
	[REST_ID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[ProductType] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ITEM_YEAR] [int] NOT NULL,
	[OPEN_QTY] [decimal](8, 2) NULL,
	[RC_QTY_01] [decimal](8, 2) NULL,
	[SL_QTY_01] [decimal](8, 2) NULL,
	[AJ_QTY_01] [decimal](8, 2) NULL,
	[RC_QTY_02] [decimal](8, 2) NULL,
	[SL_QTY_02] [decimal](8, 2) NULL,
	[AJ_QTY_02] [decimal](8, 2) NULL,
	[RC_QTY_03] [decimal](8, 2) NULL,
	[SL_QTY_03] [decimal](8, 2) NULL,
	[AJ_QTY_03] [decimal](8, 2) NULL,
	[RC_QTY_04] [decimal](8, 2) NULL,
	[SL_QTY_04] [decimal](8, 2) NULL,
	[AJ_QTY_04] [decimal](8, 2) NULL,
	[RC_QTY_05] [decimal](8, 2) NULL,
	[SL_QTY_05] [decimal](8, 2) NULL,
	[AJ_QTY_05] [decimal](8, 2) NULL,
	[RC_QTY_06] [decimal](8, 2) NULL,
	[SL_QTY_06] [decimal](8, 2) NULL,
	[AJ_QTY_06] [decimal](8, 2) NULL,
	[RC_QTY_07] [decimal](8, 2) NULL,
	[SL_QTY_07] [decimal](8, 2) NULL,
	[AJ_QTY_07] [decimal](8, 2) NULL,
	[RC_QTY_08] [decimal](8, 2) NULL,
	[SL_QTY_08] [decimal](8, 2) NULL,
	[AJ_QTY_08] [decimal](8, 2) NULL,
	[RC_QTY_09] [decimal](8, 2) NULL,
	[SL_QTY_09] [decimal](8, 2) NULL,
	[AJ_QTY_09] [decimal](8, 2) NULL,
	[RC_QTY_10] [decimal](8, 2) NULL,
	[SL_QTY_10] [decimal](8, 2) NULL,
	[AJ_QTY_10] [decimal](8, 2) NULL,
	[RC_QTY_11] [decimal](8, 2) NULL,
	[SL_QTY_11] [decimal](8, 2) NULL,
	[AJ_QTY_11] [decimal](8, 2) NULL,
	[RC_QTY_12] [decimal](8, 2) NULL,
	[SL_QTY_12] [decimal](8, 2) NULL,
	[AJ_QTY_12] [decimal](8, 2) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Group_Permissions]    Script Date: 03/08/2014 22:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Group_Permissions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Group_Permissions](
	[PermissionID] [int] NOT NULL,
	[UserGroupID] [int] NULL,
	[MenuID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL
) ON [PRIMARY]
END

GO
SET ANSI_PADDING OFF
GO
--select 'If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =' + convert(varchar(20),permissionid)+')'
--,'INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID]) 
--VALUES ('+convert(varchar(20),permissionid)+' ,'+convert(varchar(20),usergroupid)+','
--+convert(varchar(20),menuid)+' ,
--CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)'
--from omni_Group_Permissions
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =540)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (540 ,1,1 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =541)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (541 ,1,11 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =542)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (542 ,1,26 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =543)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (543 ,1,29 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =544)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (544 ,1,25 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =545)
	INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (545 ,1,2 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =546)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (546 ,1,6 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =547)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (547 ,1,9 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =548)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (548 ,1,7 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =549)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (549 ,1,10 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =550)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (550 ,1,8 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =551)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (551 ,1,32 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =552)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (552 ,1,33 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =553)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (553 ,1,37 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =554)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (554 ,1,34 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =555)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (555 ,1,38 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =556)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (556 ,1,15 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =557)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (557 ,1,16 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =558)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (558 ,1,35 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =559)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (559 ,1,36 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =560)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (560 ,1,17 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =561)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (561 ,1,18 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =562)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (562 ,1,39 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =563)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (563 ,1,40 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =564)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (564 ,1,41 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =565)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (565 ,1,42 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =566)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (566 ,1,43 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =567)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (567 ,1,44 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =568)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (568 ,1,3 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =569)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (569 ,1,21 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =570)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (570 ,1,22 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =571)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (571 ,1,23 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =572)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (572 ,1,24 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =573)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (573 ,1,12 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =574)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (574 ,1,13 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =575)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (575 ,1,14 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =576)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (576 ,1,19 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =577)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (577 ,1,20 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =578)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (578 ,1,4 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =579)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (579 ,1,28 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =580)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (580 ,1,71 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =581)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (581 ,1,60 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =582)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (582 ,1,48 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =583)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (583 ,1,52 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =584)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (584 ,1,62 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =585)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (585 ,1,55 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =586)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (586 ,1,45 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =587)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (587 ,1,68 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =588)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (588 ,1,56 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =589)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (589 ,1,70 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =590)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (590 ,1,53 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =591)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (591 ,1,27 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =592)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (592 ,1,54 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =593)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (593 ,1,30 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =594)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (594 ,1,64 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =595)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (595 ,1,66 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =596)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (596 ,1,58 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =597)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (597 ,1,59 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =598)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (598 ,1,61 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =599)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (599 ,1,63 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =600)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (600 ,1,65 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =601)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (601 ,1,67 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =602)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (602 ,1,47 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =603)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (603 ,1,5 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =604)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (604 ,1,57 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =605)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (605 ,1,72 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =606)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (606 ,1,73 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =607)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (607 ,1,74 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =608)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (608 ,1,75 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =609)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (609 ,1,76 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =610)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (610 ,1,77 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =611)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (611 ,1,78 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =612)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (612 ,1,79 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =613)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (613 ,1,80 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =614)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (614 ,1,81 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =615)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (615 ,1,82 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =616)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (616 ,1,83 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =617)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (617 ,1,84 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
If NOT Exists(Select 1 from [omni_Group_Permissions] where PermissionID =618)	
INSERT into [dbo].[omni_Group_Permissions] ([PermissionID] ,[UserGroupID],[MenuID],[CreatedOn] ,[CreatedByUserID] ,[ModifiedOn],[ModifiedByUserID])   VALUES (618 ,1,85 ,  CAST(0x0000A15400000000 AS DateTime),3 ,NULL,NULL)
GO


/****** Object:  Table [dbo].[omni_UnitMaster]    Script Date: 03/08/2014 22:59:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Device]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Device](
	[DeviceID] [int] NOT NULL,
	[DeviceName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PrinterID] [int] NULL,
	[Rest_ID] [int] NULL,
	[IsActive] [tinyint] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[DeletedOn] [datetime] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Customers]    Script Date: 03/08/2014 22:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Customers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Customers](
	[CustomerId] [int] NOT NULL,
	[FirstName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Phone] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fax] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address1] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address2] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Zipcode] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[Rest_ID] [int] NULL,
	[IsActive] [int] NULL,
	[Points] [decimal](18, 2) NULL,
	[LastOrderID] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShouldSendOfferNotices] [bit] NULL,
	[CreditLimit] [decimal](18, 2) NULL,
	[AllowCredit] [bit] NULL,
	[TotalPurchaseAmount] [decimal](18, 2) NULL,
	[TotalCreditAmount] [decimal](18, 2) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Courses]    Script Date: 03/08/2014 22:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Courses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Courses](
	[CourseID] [int] NOT NULL,
	[CourseName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SortOrder] [tinyint] NULL,
	[Rest_ID] [int] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_Cooking_Options]    Script Date: 03/08/2014 22:59:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_Cooking_Options]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_Cooking_Options](
	[OptionID] [int] NOT NULL,
	[OptionName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_ID] [int] NULL,
	[CreatedByUserID] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedOn] [datetime] NULL,
	[IsActive] [tinyint] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[omni_CompanyInfo]    Script Date: 03/08/2014 22:59:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[omni_CompanyInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[omni_CompanyInfo](
	[CompanyId] [int] NOT NULL,
	[CompanyName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address] [varchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Phone] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rest_id] [int] NULL,
	[Fax] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GlobalTax] [decimal](9, 2) NULL,
	[ABNNumber] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CurrencySymbol] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateFormat] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateFormatSQL] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedByUserID] [int] NULL,
	[ModifiedByUserID] [int] NULL,
	[ModifiedOn] [datetime] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tmp_omni_TimeZone]    Script Date: 03/08/2014 22:59:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tmp_omni_TimeZone]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tmp_omni_TimeZone](
	[ID] [int] NOT NULL,
	[TimeZoneID] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TimeZoneDisplayName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[getTableColumn]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getTableColumn]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getTableColumn](
@TblName varchar(50),
@param varchar(50),
@val varchar(50),
@getfldname varchar(100), 
@whfld varchar(50),
@whval varchar(50)
)
AS
BEGIN
Declare @sqlstr varchar(8000);
Declare @addl varchar(1000);
Declare @add2 varchar(1000);

	set @sqlstr = (''select CAST('' + @getfldname + '' as varchar(15)) as fldname from '' + @TblName);
            
    if (@whfld <>'''' and @whval <>'''')
        set @addl = '' Where '' + @whfld + ''='' +  @whval;  

	if (@param <>'''' and @val <>'''')
      set @add2 = '' And '' + @param + ''='' + @val;
      
     set @sqlstr = @sqlstr + @addl + @add2;
    
    EXECUTE(@sqlstr)
    
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getCurrentDate]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getCurrentDate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getCurrentDate]
	@CurrDate DateTime  OUTPUT
AS
--SELECT 	@CurrDate = (select GETDATE())

SELECT 	@CurrDate = (select Convert(VARCHAR(10),GETDATE(),101) + '' '' + Convert(VARCHAR(8),GETDATE(),108))





' 
END
GO
/****** Object:  StoredProcedure [dbo].[getCourseDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getCourseDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getCourseDetails]
	@CourseID			int,
	@CourseName		varchar(100) OUTPUT,
	@SortOrder		int = 0 OUTPUT,	
	@Status int OUTPUT
AS
SELECT @CourseName = CourseName,  @Status = IsActive, @SortOrder = SortOrder
FROM omni_Courses WHERE CourseID= @CourseID




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getCookingOptionDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getCookingOptionDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getCookingOptionDetails]
	@OptionID			int,
	@OptionName		varchar(100) OUTPUT,
	@Status int OUTPUT
AS
SELECT @OptionName = OptionName,  @Status = IsActive
FROM omni_Cooking_Options WHERE OptionID= @OptionID




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getCompanyDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getCompanyDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getCompanyDetails]
@CompanyName		VarChar(50) OUTPUT,
@Address			VarChar(2000) OUTPUT,
@Email				VarChar(100) OUTPUT,
@Phone				VarChar(50) OUTPUT,
@Fax				VarChar(50) OUTPUT,
@ABNNo				VarChar(25) OUTPUT,
@Tax				Decimal(7,2) OUTPUT,
@Currency			VarChar(5) OUTPUT,
@DateFormat			VarChar(20) OUTPUT	
	
AS
SELECT @CompanyName = CompanyName, @Address = Address, @Email = Email,
@Phone= Phone,	@Fax= Fax, @ABNNo= ABNNumber,  @Tax = GlobalTax,
@Currency = CurrencySymbol, @DateFormat = [DateFormat] FROM omni_CompanyInfo




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getCategoryDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getCategoryDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getCategoryDetails]
	@CategoryID int,
	@CategoryName varchar(50) OUTPUT,
	@ParentID int OUTPUT,
	@Status int OUTPUT,
	@Name2 varchar(100) output,
	@SortOrder int output
AS
SELECT 	@CategoryName = CategoryName, @ParentID = parentid,	@Status = Isactive, @Name2=Name2, @SortOrder = SortOrder 
FROM omni_Item_Categories WHERE CategoryID = @CategoryID




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getSupplierDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getSupplierDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getSupplierDetails]
	@SuppID int,
	@FirstName varchar(50) OUTPUT,
	@LastName varchar(50) OUTPUT,
	@Email varchar(50) OUTPUT,
	@Phone varchar(50) OUTPUT,
	@Address1 varchar(255) OUTPUT,
	@Address2 varchar(255) OUTPUT,
	@ZipCode varchar(25) OUTPUT,
	@Status int OUTPUT
AS
SELECT 	@FirstName = FirstName, @LastName = LastName, @Email=Email, @Phone= Phone,@Address1 = Address1,@Address2=Address2,@ZipCode=Zipcode,	@Status = Isactive
FROM omni_Suppliers WHERE SupplierId = @SuppID





' 
END
GO
/****** Object:  StoredProcedure [dbo].[getSubRecipeDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getSubRecipeDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getSubRecipeDetails]
	@SubRecipeId			int,
	@SubRecipeName	varchar(150) OUTPUT,
	@UnitID					int OUTPUT,
	@Status			 int OUTPUT
AS
SELECT 	@SubRecipeName= SubRecipeName,@UnitID = UnitID, @Status = Isactive 
FROM omni_SubRecipe WHERE  SubRecipeID =@SubRecipeId


' 
END
GO
/****** Object:  StoredProcedure [dbo].[getSubRecipe_Mixing_Options]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getSubRecipe_Mixing_Options]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getSubRecipe_Mixing_Options]
	@SubRecipeID				int ,
	@IngID						int,
	@Qty						decimal(6,2) OUTPUT,
	@RestID						int,
	@flag					    bit OUTPUT  

AS
Declare @count as int
set @count = (SELECT count(*) as totcnt  from omni_SubRecipe_Mixing_Details 
where SubRecipeID=@SubRecipeID and IngredientId = @IngID and Rest_ID=@RestID)

set @Qty = (SELECT Qty from omni_SubRecipe_Mixing_Details 
where SubRecipeID=@SubRecipeID and IngredientId = @IngID and Rest_ID=@RestID)

if (@count=0) 
	set @flag = 0
ELSE
	set @flag = 1

' 
END
GO
/****** Object:  StoredProcedure [dbo].[getStateDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getStateDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getStateDetails]
	@StateID int,
	@StateName varchar(50) OUTPUT,
	@Status int OUTPUT
AS
SELECT 	@StateName = StateName,	@Status = Isactive
FROM omni_State WHERE StateID = @StateID
 




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getRestaurantDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getRestaurantDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getRestaurantDetails]
	@RestID int,
	@RestName varchar(150) OUTPUT,
	@Initial char(5) OUTPUT,
	@Address1 varchar(255) OUTPUT,	
	@Address2 varchar(255) OUTPUT,		
	@City	  varchar(50) OUTPUT,
	@State	  int OUTPUT,
	@Zip	  varchar(50)  OUTPUT,
	@Phone	  VarChar(50) OUTPUT,
	@Fax	  VarChar(50) OUTPUT,
	@Email	  VarChar(255) OUTPUT,
	@WebSite  VarChar(100) OUTPUT,	
	@TablesCount	int OUTPUT,
	--@Header	  VARCHAR(255) OUTPUT,
	@Footer1	  VARCHAR(1000) OUTPUT,
	@Footer2      VARCHAR(1000) OUTPUT,
	@KitchenView  bit OUTPUT,
	@ExpediteView bit OUTPUT,	
	@Tax bit OUTPUT,	
	@Status int OUTPUT,
	
	@Header_Name varchar(255) = null OUTPUT,
	@Header_Address1 VarChar(1000) = null OUTPUT,
	@Header_City VarChar(50) = null OUTPUT,
	@Header_State VarChar(50) = null OUTPUT,
	@Header_Zip VarChar(20) = null OUTPUT,
	@Header_Phone VarChar(20) = null OUTPUT,
	@Header_ABN VarChar(25) = null OUTPUT,
	@Header_TaxInvoice Bit =false OUTPUT,
	@Header_Website VarChar(100) = null OUTPUT,
	@Header_Email VarChar(100) = null	 OUTPUT
AS

SELECT @RestName = RestName, @Initial = Initials, @Address1 = Address1,  
@Address2 = Address2, @City = City, @State = [StateID], @Zip = Zip,
@Phone = Phone, @Fax = Fax, @Email = Email,@WebSite = Website, @TablesCount = TablesCount,
@Footer1 = Footer1, @Footer2= Footer2, 
@KitchenView = (case when KitchenView=1 then 1 else 0 end),
@ExpediteView = (case when ExpediteView=1 then 1 else 0 end),
@Tax = (case when Tax=1 then 1 else 0 end),
@Status = IsActive,

@Header_Name = Header_Name,@Header_Address1 = Header_Address1,
@Header_City = Header_City, @Header_State = Header_State,
@Header_Zip = Header_Zip, @Header_Phone = Header_Phone,
@Header_ABN = Header_ABN, @Header_TaxInvoice = Header_TaxInvoice,
@Header_Website = Header_Website, @Header_Email = Header_Email


FROM omni_Restuarnt_info WHERE Rest_id = @RestID








' 
END
GO
/****** Object:  StoredProcedure [dbo].[getRestaurant_Settings_Detail]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getRestaurant_Settings_Detail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getRestaurant_Settings_Detail]
@RestID int,
@CustomerView bit OUTPUT,
@AddLines_Between_Order_Item  bit output,
@Sort_Course_By tinyint output,
@Print_Transferred_Order bit output,
@Print_Deleted_Order bit output ,
@Print_Voided_Items bit output,
@KitchenView_Timeout tinyint output,
@Allow_Void_Items bit output,
@Allow_Delete_Order bit output,
@Quick_Service tinyint output,
@Table_Layout_Type tinyint output,
@Auto_Prompt_Tip bit output,
@Sort_Items_By tinyint output,
@Sort_SubCat_By tinyint output,
@Sort_Products tinyint output,
@No_Of_Devices tinyint output,
@Table_Layout bit output,
@Email_Z_Report bit output,
@Notify_No_Sale bit output,
@ScannerMode	tinyint output,
@ShouldEnableClockIn	bit output,
@HoldAndFire	tinyint output,
@CashDrawerBalancing bit output,
@NoSaleLimit int output

AS

SELECT @CustomerView = CustomerView, @AddLines_Between_Order_Item = AddLines_Between_Order_Item, @Sort_Course_By = Sort_Course_By, @Print_Transferred_Order = Print_Transferred_Order, @Print_Deleted_Order = Print_Deleted_Order, @Print_Voided_Items = Print_Voided_Items, @KitchenView_Timeout = Kitchen_View_Timeout, @Allow_Void_Items = Allow_Void_Order_Item,@Allow_Delete_Order = Allow_Delete_Send_Order, @Quick_Service = Quick_Service, @Table_Layout_Type = Table_Layout_Type, @Auto_Prompt_Tip = Auto_Prompt_Tip, @Sort_Items_By = Sort_Items_By, @Sort_SubCat_By = Sort_SubCategories_By, @Sort_Products = Sort_Products_By, @No_Of_Devices = No_Of_Devices, @Table_Layout = Use_Table_Layout, @Email_Z_Report = shouldEmailZReport, @Notify_No_Sale = shouldNotifyNoSale, @ScannerMode = scannerMode, @ShouldEnableClockIn = ShouldEnableClockIn,
	@HoldAndFire = HoldAndFire, @CashDrawerBalancing = CashDrawerBalancing, @NoSaleLimit = NoSaleLimit  
FROM omni_Restuarnt_info_Settings WHERE Rest_id = @RestID



' 
END
GO
/****** Object:  StoredProcedure [dbo].[getProductModifiers]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProductModifiers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getProductModifiers]
	@ProductID				int ,
	@ModifierID				int,
	@flag					bit OUTPUT  

AS
Declare @count as int
set @count = (SELECT count(*) as totcnt  from omni_Product_Modifiers 
where ProductID=@ProductID and ModifierID = @ModifierID)

if (@count=0) 
	set @flag = 0
ELSE
	set @flag = 1



' 
END
GO
/****** Object:  StoredProcedure [dbo].[getProductDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProductDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getProductDetails]
	@ProductID				int ,
	@CategoryID				int  OUTPUT,
	@ProductName			varchar(50) OUTPUT,
	@ProductName2			varchar(50) OUTPUT,
	@ProductImagePath		varchar(255) OUTPUT,
	@ProductDesc			varchar(5000) OUTPUT,
	@ProductColor			char(10) OUTPUT,
	@BarCode				varchar(50) OUTPUT,
	@GST					bit OUTPUT,
	@HasFlag				bit OUTPUT,
	@HasOpenPrice			bit OUTPUT,
	@IsDailyItem			bit OUTPUT,
	@IsSoleProduct			bit OUTPUT,
	@ProductPrice1			decimal(7,2) OUTPUT,
	@ProductPrice2			decimal(7,2) OUTPUT,
	@ProductPrice3			decimal(7,2) OUTPUT,
	@ProductPrice4			decimal(7,2) OUTPUT,
	@ProductPrice5			decimal(7,2) OUTPUT,	
	@OpQty					decimal(7,2) OUTPUT,
	@StockInHand			decimal(7,2) OUTPUT,
	@Status					int OUTPUT,
	@ParentID				int OUTPUT,
	@ChangePrice			bit output,
	@SortOrder				int output,
	@CourseID				int output,
	@SupplierID				int output,
	@UnitID					int output,
	@ReOrdLvl				decimal(7,2) output,
	@ReOrdQty				decimal(7,2) output,
	@Points					decimal(7,2) OUTPUT

	
AS
SELECT @ProductName = ProductName, @ProductName2 = ProductName2,@ProductImagePath = ProductImageWithPath, @ProductDesc = ProductDescription,
@ProductColor = Color, @BarCode = Barcode, @GST = GST, @HasFlag =Hasflag, @HasOpenPrice = HasOpenPrice,@IsDailyItem = IsDailyItem ,@IsSoleProduct = IsSoleProduct, @Status = a.IsActive,
@ProductPrice1 = Price1, @ProductPrice2 = Price2,@ProductPrice3 = Isnull(Price3,0.00), @ProductPrice4 = Isnull(Price4,0.00), @ProductPrice5 = IsNull(Price5,0.00),@OpQty = IsNull(OpQty,0.00) ,@StockInHand = StockInHand ,
@CategoryID = a.CategoryID, @ParentID = ParentId, @ChangePrice = ChangePrice, @SortOrder = a.SortOrder, @CourseID = CourseID,@SupplierID = SupplierID,@UnitID =UnitID, @Points = Points,
@ReOrdLvl = IsNull(ReOrderLevel,0), @ReOrdQty = IsNull(ReOrderQty,0.00) FROM omni_Products a 
inner join omni_Item_Categories b on (a.CategoryID = b.CategoryId) and 
ProductID = @ProductID


' 
END
GO
/****** Object:  StoredProcedure [dbo].[getProductCookingOptions]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProductCookingOptions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getProductCookingOptions]
	@ProductID				int ,
	@OptionID				int,
	@flag					bit OUTPUT  

AS
Declare @count as int
set @count = (SELECT count(*) as totcnt  from omni_Product_Cooking_Options 
where ProductID=@ProductID and OptionID = @OptionID)

if (@count=0) 
	set @flag = 0
ELSE
	set @flag = 1



' 
END
GO
/****** Object:  StoredProcedure [dbo].[getProduct_Printer_Option]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProduct_Printer_Option]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getProduct_Printer_Option]
	@ProductID				int ,
	@PrinterID				int OUTPUT,	
	@flag					bit OUTPUT  

AS

Declare @count as int
set @count = (SELECT count(*) as totcnt  from omni_Product_Kitchen_Printer_Options 
where ProductID=@ProductID and OptionID = @PrinterID and OptionType= ''P'')

if (@count=0) 
	set @flag = 0
ELSE
	set @flag = 1



' 
END
GO
/****** Object:  StoredProcedure [dbo].[getProduct_Mixing_Options]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProduct_Mixing_Options]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getProduct_Mixing_Options]
	@ProductID				int ,
	@IngID					int,
	@MixingType			char(25),
	@flag					bit OUTPUT  

AS
Declare @count as int
set @count = (SELECT count(*) as totcnt  from omni_Product_Ingredient_SubreciepeDetails 
where ProductID=@ProductID and IngredientId = @IngID and MixingType=@MixingType)

if (@count=0) 
	set @flag = 0
ELSE
	set @flag = 1

' 
END
GO
/****** Object:  StoredProcedure [dbo].[getProduct_Kitchen_Printer_Option]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProduct_Kitchen_Printer_Option]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getProduct_Kitchen_Printer_Option]
	@ProductID				int ,
	@KitchenID				int OUTPUT,
	@PrinterID				int OUTPUT	

AS


SELECT @KitchenID = (select OptionID from omni_Product_Kitchen_Printer_Options where
ProductID = @ProductID and OptionType=''K''),
@PrinterID = (select OptionID from omni_Product_Kitchen_Printer_Options where
ProductID = @ProductID and OptionType=''P'')




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getProduct_Kitchen_Option]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getProduct_Kitchen_Option]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getProduct_Kitchen_Option]
	@ProductID				int ,
	@KitchenID				int OUTPUT
AS


SELECT @KitchenID = (select OptionID from omni_Product_Kitchen_Printer_Options where
ProductID = @ProductID and OptionType=''K'')




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getPrinterDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getPrinterDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getPrinterDetails]
	@PrinterID int,
	@PrinterName		varchar(50) OUTPUT,
	@IPAddress			varchar(50) OUTPUT,
	@PosOrItem			char(1) OUTPUT,	
	@IsPrintIPAddress	char(1) OUTPUT,
	@Status int OUTPUT,
	@PrinterType	char(1) output,
	@NoOfCopies		int = 0 output,
	@Trigger_Cash_Drawer	tinyint = 0 output

AS
SELECT @PrinterName = PrinterName, @IPAddress= IPAddress,  
@IsPrintIPAddress = IsPrintIPAddress,@PosOrItem=PosorItem,   @Status = IsActive,
@PrinterType = PrinterType, @NoOfCopies= NoOfCopies, @Trigger_Cash_Drawer=Trigger_Cash_Drawer 
FROM omni_Printers WHERE PrinterId = @PrinterID




' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetOrderPaymentModeStatus]    Script Date: 03/08/2014 23:00:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrderPaymentModeStatus]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GetOrderPaymentModeStatus] (@OrderID varchar(50),@currency varchar(10))
RETURNS VARCHAR(200)
AS
BEGIN
   DECLARE @HandlingCode VARCHAR(1000)
   DECLARE @ReturnValue  VARCHAR(2000)

-- use that fastest cursor methods: local fast_forward
   DECLARE code_cursor CURSOR LOCAL fast_forward FOR
    select @currency + CAST(PaidAmount as varchar(50)) + '' ->'' + case when b.PaymentType=''CashSale'' then ''CSH'' else case when b.PaymentType=''CardSale'' then ''CC'' else ''Vcr'' end end as PayStatus from omni_OrderPaymentInfo a 
	inner join omni_PaymentType b on a.PaymentTypeID = b.PaymentTypeID 
	where a.Order_TranID=@OrderID


   SET @ReturnValue = ''''  -- set to non null

   OPEN code_cursor
   FETCH NEXT FROM code_cursor  INTO @HandlingCode
   WHILE (@@FETCH_STATUS = 0)
   BEGIN
       SET @ReturnValue = @ReturnValue + @HandlingCode + '', '' 

       IF LEN (@ReturnValue) > 1000 BREAK -- avoid overflow

       FETCH NEXT FROM code_cursor INTO @HandlingCode
   END

   CLOSE code_cursor
   DEALLOCATE code_cursor

-- remove last delimiter
   IF LEN(@ReturnValue) > 1 SET @ReturnValue = SUBSTRING(@ReturnValue,1,LEN(@ReturnValue)-1)

   --set @ReturnValue = (select LEFT(@ReturnValue, LEN(@ReturnValue) - 1))	
   
   RETURN @ReturnValue

end

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[getOrderIDSWithinDateRangeNew]    Script Date: 03/08/2014 23:00:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getOrderIDSWithinDateRangeNew]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[getOrderIDSWithinDateRangeNew] 
(

	@FromDate DateTime,
	@TillDate DateTime,
	@RestID int
)
RETURNS varchar(8000)
AS
BEGIN

	DECLARE @XYList varchar(8000)
	DECLARE @OrderID varchar(50)
	
	SET @XYList = ''''
	
	DECLARE db_cursor CURSOR FOR  
	SELECT Order_TranID FROM omni_OrderInfo where TransactionDate>=@FromDate and TransactionDate<=@TillDate and Rest_id=@RestID

	OPEN db_cursor  
	FETCH NEXT FROM db_cursor INTO @OrderID  

	WHILE @@FETCH_STATUS = 0  
	BEGIN  
       SET @XYList = @XYList + @OrderID  + '',''

       FETCH NEXT FROM db_cursor INTO @OrderID  
	END

	set @XYList = (select LEFT(@XYList, LEN(@XYList) - 1))
	
	CLOSE db_cursor  
	DEALLOCATE db_cursor 
	
	return @XYList
END


' 
END
GO
/****** Object:  StoredProcedure [dbo].[getOrder_Note]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getOrder_Note]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getOrder_Note]
@NoteID int = 0,
@Message			varchar(8000) OUTPUT,
@Status				int OUTPUT
	
AS
SELECT @Message = [Message], @Status = IsActive  FROM omni_Order_Note where NoteID =@NoteID




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getOption_Settings]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getOption_Settings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getOption_Settings]
@OptionName			varchar(50)=null,
@OptionValue		varchar(50)=null OUTPUT

AS

BEGIN

select @OptionValue = OptionValue from omni_Option_Settings where OptionName=@OptionName

END






' 
END
GO
/****** Object:  StoredProcedure [dbo].[getModifierLevelDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getModifierLevelDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATe PROCEDURE [dbo].[getModifierLevelDetails]
	@LevelID int,
	@ModifierLevelName varchar(50) OUTPUT,
	@Status int OUTPUT
AS
SELECT 	@ModifierLevelName = ModifierLevelName,	@Status = Isactive
FROM omni_Modifiers_Level WHERE  LevelID = @LevelID




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getModifierDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getModifierDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getModifierDetails]
	@ModifierId			int,
	@ModifierName varchar(50) OUTPUT,
	@Name2 varchar(50) OUTPUT,
	@Price1 decimal(9,2) OUTPUT,
	@Price2 decimal(9,2) OUTPUT,
	@ModifierLevelID int OUTPUT,
	@SortOrder int OUTPUT,
	@GST			 bit OUTPUT,
	@Status			 int OUTPUT
AS
SELECT 	@ModifierName = ModifierName, @Name2 = NAME2,	@Status = Isactive,
@Price1 = Price1,@Price2 = Price2, @SortOrder = SortOrder, @ModifierLevelID = ModifierLevelID, 
@GST= GST FROM omni_Modifiers WHERE  ModifierID =@ModifierID




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getKitchenDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getKitchenDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATe PROCEDURE [dbo].[getKitchenDetails]
	@KitchenID			int,
	@KitchenName		varchar(50) OUTPUT,
	@Status int OUTPUT
AS
SELECT @KitchenName = KitchenName,  @Status = IsActive
FROM omni_Kitchen WHERE KitchenId= @KitchenID




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getKitchen_Instruction]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getKitchen_Instruction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getKitchen_Instruction]
@InstructionID	Int,
@Message			varchar(8000) OUTPUT,
@Status				int OUTPUT
	
AS
SELECT @Message = [Message], @Status = IsActive FROM omni_Kitchen_Instruction where InstructionID = @InstructionID




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getIngredientDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getIngredientDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getIngredientDetails]
	@IngredientID			int,
	@IngredientName varchar(150) OUTPUT,
	@Price decimal(6,2) OUTPUT,
	@UnitID int OUTPUT,
	@BarCode varchar(50) OUTPUT,
	@OpQty Decimal(7,2) OUTPUT,
	@ReOrdQty Decimal(7,2) OUTPUT,
	@ReOrdLvl Decimal(7,2) OUTPUT,
	@SupplierID int OUTPUT,
	@Status int OUTPUT,
	@IsDailyItem bit OUTPUT
AS
SELECT 	@IngredientName = IngredientName, 
@Price = Price,@UnitID = UnitID,
@BarCode= BarCode,@OpQty = OpQty ,@ReOrdQty = ReOrderQty ,@ReOrdLvl=ReOrderLevel ,@SupplierID=SupplierID ,@Status = Isactive, @IsDailyItem = IsDailyItem
FROM omni_Items_Ingredients WHERE  IngredientId  =@IngredientID


' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetIngredient_SubRcpUnit]    Script Date: 03/08/2014 23:00:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetIngredient_SubRcpUnit]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GetIngredient_SubRcpUnit] 
(@ID int,@ProductID int,@type varchar(20), @RestID int)
RETURNS Varchar(50)
AS
BEGIN
   DECLARE @UnitName varchar(50)
   DECLARE @UnitID int	
-- use that fastest cursor methods: local fast_forward
   
   if @type=''Ingredient''
    select @UnitID = UnitID from omni_Items_Ingredients 
     where IngredientID=@ID and Rest_ID = @RestID
   else
	select @UnitID = UnitID from omni_SubRecipe 
	where SubRecipeID=@ID and Rest_ID = @RestID

   select @UnitName = UnitName from omni_UnitMaster where UnitID = @UnitID
   
   RETURN @UnitName

end


' 
END
GO
/****** Object:  StoredProcedure [dbo].[getDeviceInfo]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getDeviceInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getDeviceInfo]
	@DeviceID			int,
	@DeviceName		varchar(100) OUTPUT,
	@PrinterID	    int OUTPUT,	
	@Status int OUTPUT
AS
SELECT @DeviceName = DeviceName, @PrinterID = PrinterID,  @Status = IsActive
FROM  omni_Device WHERE DeviceID = @DeviceID




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getCustomerDetails]    Script Date: 03/08/2014 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getCustomerDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getCustomerDetails]
	@CustID int,
	@FirstName varchar(50) OUTPUT,
	@LastName varchar(50) OUTPUT,
	@Email varchar(50) OUTPUT,
	@Phone varchar(50) OUTPUT,
	@Address1 varchar(255) OUTPUT,
	@Address2 varchar(255) OUTPUT,
	@ZipCode varchar(25) OUTPUT,
	@Status int OUTPUT,
	@Points decimal(7,2) = 0 OUTPUT
AS
SELECT 	@FirstName = FirstName, @LastName = LastName, @Email=Email, @Phone= Phone,@Address1 = Address1,@Address2=Address2,@ZipCode=Zipcode,	@Status = Isactive, @Points = Points 
FROM omni_Customers WHERE CustomerId = @CustID





' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_user_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_user_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_user_Update]
@UserID			int = 0,
@FirstName		varchar(50) = null,
@LastName		varchar(50) = null,
@UserPhone VarChar(20) = null,
@StartDate DateTime ,
@UserAlias		varchar(50) = null,
@UserName		varchar(50) = null,
@UserEmail		varchar(50) = null,
@UserPassword	binary(50),
@UserGroupID	int = 0,
@LoggedUserID	int = 0,
@RestID			int = 0,
@sDate			datetime,
@Status			int = 0,
@Mode  			nvarchar(20)=null,
@PassCheck		char(1),
@UserPin		int	= 0,
@HourlyRate		decimal(7,2) = 0.00

AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_users(FirstName, LastName,UserAlias,UserPhone, StartDate ,UserName, UserEmail,UserPassword,UserGroupID,CreatedByUserID, CreateDate,Rest_id,IsActive, UserPin, HourlyRate )
 values(@FirstName, @LastName,@UserAlias, @UserPhone, @StartDate ,@UserName, @UserEmail,@UserPassword ,@UserGroupID , @LoggedUserID, @sDate, @RestID ,@Status, @UserPin, @HourlyRate)
END

IF @Mode=''edit''

BEGIN

if @PassCheck=''0''
	update omni_users set FirstName = @FirstName, LastName = @LastName, UserPhone= @UserPhone, StartDate= @StartDate, UserAlias = @UserAlias, UserName = @UserName, UserEmail = @UserEmail,
	UserGroupID = @UserGroupID, ModifyDate = @sDate,
	ModifiedByUserID=@LoggedUserID, Rest_id = @RestID, IsActive =@Status ,
	UserPin = @UserPin, HourlyRate = @HourlyRate where UserID=@UserID
else
	update omni_users set FirstName = @FirstName, LastName = @LastName, UserPhone = @UserPhone, StartDate=@StartDate, UserAlias = @UserAlias, UserName = @UserName, UserEmail = @UserEmail,
	UserPassword = @UserPassword, UserGroupID = @UserGroupID, ModifyDate = @sDate,
	ModifiedByUserID=@LoggedUserID, Rest_id = @RestID,HourlyRate=@HourlyRate, IsActive =@Status,
	UserPin =@UserPin where UserID=@UserID	
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_user_group_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_user_group_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_user_group_Update]
@UserGroupID	int = 0,
@UserGroupName	varchar(50) = null,
@LoggedUserID int = 0,
@sDate		datetime,
--@ModifyDate		datetime = ''01/01/1900'',
--@DeleteDate		datetime = ''01/01/1900'',
@Status		int = 0,
@Mode  				nvarchar(20)=null
AS

IF @Mode=''add''

BEGIN

insert into omni_user_group(UserGroupName,CreatedByUserID, CreateDate,IsActive)
 values(@UserGroupName, @LoggedUserID, @sDate,@Status)
END

IF @Mode=''edit''

BEGIN

update omni_user_group set UserGroupName = @UserGroupName,
ModifiedByUserID=@LoggedUserID, ModifyDate = @sDate,IsActive =@Status where
UserGroupID=@UserGroupID
END

IF @Mode=''del''

BEGIN

delete from  omni_user_group  where UserGroupID=@UserGroupID
delete from omni_Group_Permissions where UserGroupID=@UserGroupID

END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_UnitMaster_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_UnitMaster_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_UnitMaster_Update]
@UnitID	int = 0,
@UnitName	varchar(50) = null,
@Rest_ID int = 0,
@LoggedUserID int = 0,
@sDate		datetime,
--@ModifyDate		datetime = ''01/01/1900'',
--@DeleteDate		datetime = ''01/01/1900'',
@Status		int = 0,
@Mode  				nvarchar(20)=null
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_UnitMaster(UnitName,Rest_ID,CreatedByUserID, CreatedOn,IsActive)
 values(@UnitName,@Rest_ID, @LoggedUserID, @sDate,@Status)
END

IF @Mode=''edit''

BEGIN

update omni_UnitMaster set UnitName = @UnitName,
ModifiedByUserID=@LoggedUserID, ModifiedOn=@sDate,IsActive =@Status where
UnitID=@UnitID and Rest_ID = @Rest_ID
END

IF @Mode=''del''

BEGIN
delete from  omni_UnitMaster where UnitID=@UnitID

END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_TaxInfo_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_TaxInfo_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_TaxInfo_Update]
@TaxInfoID					int = 0,
@Literal					varchar(100) = null,
@TaxRate					decimal(6,2) = 0.00,
@Rest_ID						int = 0,
@LoggedUserID int = 0,
@sDate		datetime,
--@ModifyDate		datetime = ''01/01/1900'',
--@DeleteDate		datetime = ''01/01/1900'',
@Status						int = 0,
@Mode  						nvarchar(20)=null

AS

IF @Mode=''add''

BEGIN

insert into omni_TaxInfo(TaxInfoLiteral,TaxRate,Rest_ID,CreatedByUserID, CreatedOn,IsActive)
 values(@Literal, @TaxRate,@Rest_ID, @LoggedUserID, @sDate, @Status)
END

IF @Mode=''edit''

BEGIN

update omni_TaxInfo set TaxInfoLiteral = @Literal,TaxRate = @TaxRate,Rest_ID=@Rest_ID, ModifiedByUserID=@LoggedUserID, ModifiedOn = @sDate,
IsActive =@Status where TaxInfoID = @TaxInfoID

END

IF @Mode=''del''

BEGIN

delete from omni_TaxInfo where TaxInfoID = @TaxInfoID and Rest_ID=@Rest_ID

END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Supplier_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Supplier_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_Supplier_Update]
@SuppID int  = 0,
@FirstName VarChar(50) = null,
@LastName VarChar(50) = null,
@Email VarChar(100) = null,
@Address1 VarChar(255) = null,
@Address2 VarChar(255) = null,
@Phone VarChar(25) = null,
@ZipCode VarChar(25) = null,
@Status int = 0,
@RestID int = 0,
@sDate DateTime,
@LoggedUserID int = 0,
@Mode VarChar(20) = null

AS

IF @Mode=''add''

BEGIN


insert into omni_Suppliers(FirstName, LastName, Email, Address1, Address2, Phone,ZipCode,CreatedOn, CreatedByUserID, IsActive)
 values(@FirstName, @LastName, @Email, @Address1, @Address2, @Phone,@ZipCode,@sDate, @LoggedUserID, @Status)
  
END

IF @Mode=''edit''

BEGIN

update omni_Suppliers set FirstName = @FirstName, LastName= @LastName, Email = @Email, Address1 = @Address1, Address2= @Address2,Phone = @Phone, ZipCode = @ZipCode,  ModifiedOn = @sDate, ModifiedByUserID = @LoggedUserID, IsActive =@Status where SupplierId = @SuppID

END

IF @Mode=''del''

BEGIN

delete from omni_Suppliers where supplierid = @SuppID 

end







' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_SubRecipe_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_SubRecipe_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_SubRecipe_Update]
@SubRecipeID	int = 0,
@SubRecipeName	varchar(150) = null,
@UnitID			int = 0,
@Rest_ID int = 0,
@LoggedUserID int = 0,
@sDate		datetime,
--@ModifyDate		datetime = ''01/01/1900'',
--@DeleteDate		datetime = ''01/01/1900'',
@Status		int = 0,
@Mode  				nvarchar(20)=null
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_SubRecipe(SubRecipeName, UnitID, Rest_ID,CreatedByUserID, CreatedOn,IsActive)
 values(@SubRecipeName,@UnitID,@Rest_ID, @LoggedUserID, @sDate,@Status)
END

IF @Mode=''edit''

BEGIN

update omni_SubRecipe set SubRecipeName = @SubRecipeName,UnitID = @UnitID,
ModifiedByUserID=@LoggedUserID, ModifiedOn=@sDate,IsActive =@Status where
SubRecipeID=@SubRecipeID and Rest_ID = @Rest_ID
END

IF @Mode=''del''

BEGIN
delete from  omni_SubRecipe where SubRecipeID=@SubRecipeID

END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_SubRecipe_Mixing_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_SubRecipe_Mixing_Options_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_SubRecipe_Mixing_Options_Update]
@SubRecipeID								int = 0,
@IngID										int = 0,
@Qty										Decimal(6,2),
@RestID										int = 0,
@Mode  								nvarchar(20)=null
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_SubRecipe_Mixing_Details(SubRecipeID,IngredientID,Qty, Rest_ID) 
 values(@SubRecipeID,@IngID,@Qty,@RestID)
END

IF @Mode=''edit''

BEGIN
delete from omni_SubRecipe_Mixing_Details where SubRecipeID = @SubRecipeID  and IngredientId = @IngID and Rest_Id=@RestID

insert into omni_SubRecipe_Mixing_Details(SubRecipeID,IngredientID,Qty, Rest_ID) 
 values(@SubRecipeID,@IngID,@Qty,@RestID)
END

IF @Mode=''del''

BEGIN

delete from omni_SubRecipe_Mixing_Details where SubRecipeID = @SubRecipeID  and IngredientId = @IngID and Rest_Id=@RestID

END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_SubCategory_Clone]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_SubCategory_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_SubCategory_Clone]
@SourceRestID	int = 0,
@TargetRestID	int = 0,
@CreatedByUserID int=0,
@CreatedOn		DateTime

AS

BEGIN

declare @cnt int

set @cnt = (select count(*) as totcnt from omni_Item_Categories where Rest_ID=@TargetRestID and CategoryName in (select CategoryName from omni_Item_Categories where Rest_ID=@SourceRestID and ParentId>0))

if (@cnt=0)  

  insert into omni_Item_Categories(
  CategoryName,Name2, ParentID, Rest_ID,CreatedByUserID,CreateDate, SortOrder,IsActive) select 
  a.CategoryName,a.Name2, (select CategoryId from omni_Item_Categories where CategoryName = b.CategoryName and Rest_ID=@TargetRestID) as ParentID,@TargetRestID as Rest_ID,@CreatedByUserID as CreatedByUserID, @CreatedOn as CreateDate, a.SortOrder,a.IsActive from omni_Item_Categories a
  inner join omni_Item_Categories b on a.ParentId = b.CategoryId 
  where a.Rest_ID=@SourceRestID and a.ParentId>0
 
 
 
  
END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_State_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_State_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_State_Update]
@StateID	int = 0,
@StateName	varchar(50) = null,
@Status		int = 0,
@Mode  				nvarchar(20)=null
AS

IF @Mode=''add''

BEGIN

insert into omni_State(StateName,IsActive)
 values(@StateName,@Status)
END

IF @Mode=''edit''

BEGIN

update omni_State set StateName= @statename,
IsActive =@Status where StateID=@StateID
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Restaurant_Settings_Info_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Restaurant_Settings_Info_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Restaurant_Settings_Info_Update]

@Rest_ID int = 0,
@CustomerView bit = false,
@AddLines_Between_Order_Item  bit = false,
@Sort_Course_By tinyint = 0,
@Print_Transferred_Order bit = false,
@Print_Deleted_Order bit = false,
@Print_Voided_Items bit = false,
@KitchenView_Timeout tinyint = 0,
@Allow_Void_Items bit = false,
@Allow_Delete_Order bit = false,
@Quick_Service tinyint = 0,
@Table_Layout_Type tinyint = 0,
@Auto_Prompt_Tip bit = false,
@Sort_Items_By tinyint = 0,
@Sort_SubCat_By tinyint = 0,
@Sort_Products tinyint = 0,
@No_Of_Devices tinyint = 0,
@Table_Layout bit = false,
@Email_Z_Report bit = false,
@Notify_No_Sale bit = false,
@Mode VarChar(20) = null,
@ScannerMode tinyint = 0,
@ShouldEnableClockIn bit = false,
@HoldAndFire	tinyint = 0,
@CashDrawerBalancing bit = false,
@NoSaleLimit int = 0

AS

IF @Mode=''add''

BEGIN

insert into omni_Restuarnt_info_Settings(CustomerView, AddLines_Between_Order_Item, Sort_Course_By, Print_Transferred_Order, Print_Deleted_Order, Print_Voided_Items, Kitchen_View_Timeout, Allow_Void_Order_Item, Allow_Delete_Send_Order, Quick_Service, Table_Layout_Type, Auto_Prompt_Tip, Sort_Items_By, Sort_SubCategories_By, Sort_Products_By, No_Of_Devices, Use_Table_Layout, shouldEmailZReport, shouldNotifyNoSale, ScannerMode, ShouldEnableClockIn, HoldAndFire, CashDrawerBalancing, NoSaleLimit, Rest_ID)
 values(@CustomerView, @AddLines_Between_Order_Item,@Sort_Course_By,@Print_Transferred_Order,@Print_Deleted_Order,@Print_Voided_Items,@KitchenView_Timeout, @Allow_Void_Items, @Allow_Delete_Order, @Quick_Service,@Table_Layout_Type, @Auto_Prompt_Tip, @Sort_Items_By,@Sort_SubCat_By, @Sort_Products,@No_Of_Devices,@Table_Layout,@Email_Z_Report,@Notify_No_Sale, @ScannerMode, @ShouldEnableClockIn, @HoldAndFire, @CashDrawerBalancing,@NoSaleLimit,@Rest_ID)
 
END

IF @Mode=''edit''

BEGIN
Declare @cnt int
set @cnt = (select COUNT(*) from omni_Restuarnt_info_Settings where Rest_ID = @Rest_ID)

if (@cnt=0) 
insert into omni_Restuarnt_info_Settings(CustomerView, AddLines_Between_Order_Item, Sort_Course_By, Print_Transferred_Order, Print_Deleted_Order, Print_Voided_Items, Kitchen_View_Timeout, Allow_Void_Order_Item, Allow_Delete_Send_Order, Quick_Service, Table_Layout_Type, Auto_Prompt_Tip, Sort_Items_By, Sort_SubCategories_By, Sort_Products_By, No_Of_Devices, Use_Table_Layout, shouldEmailZReport, shouldNotifyNoSale, ScannerMode, ShouldEnableClockIn, HoldAndFire, CashDrawerBalancing, NoSaleLimit, Rest_ID)
 values(@CustomerView, @AddLines_Between_Order_Item,@Sort_Course_By,@Print_Transferred_Order,@Print_Deleted_Order,@Print_Voided_Items,@KitchenView_Timeout, @Allow_Void_Items, @Allow_Delete_Order, @Quick_Service,@Table_Layout_Type, @Auto_Prompt_Tip, @Sort_Items_By,@Sort_SubCat_By, @Sort_Products,@No_Of_Devices,@Table_Layout,@Email_Z_Report,@Notify_No_Sale, @ScannerMode, @ShouldEnableClockIn, @HoldAndFire, @CashDrawerBalancing, @NoSaleLimit,@Rest_ID)

else
update omni_Restuarnt_info_Settings set CustomerView=@CustomerView, AddLines_Between_Order_Item = @AddLines_Between_Order_Item, Sort_Course_By=@Sort_Course_By, Print_Transferred_Order = @Print_Transferred_Order, Print_Deleted_Order = @Print_Deleted_Order, Print_Voided_Items=@Print_Voided_Items, Kitchen_View_Timeout=@KitchenView_Timeout, Allow_Void_Order_Item= @Allow_Void_Items, Allow_Delete_Send_Order = @Allow_Delete_Order, Quick_Service=@Quick_Service, Table_Layout_Type = @Table_Layout_Type, Auto_Prompt_Tip = @Auto_Prompt_Tip, Sort_Items_By = @Sort_Items_By, Sort_SubCategories_By = @Sort_SubCat_By, Sort_Products_By = @Sort_Products, No_Of_Devices = @No_Of_Devices, Use_Table_Layout = @Table_Layout, shouldEmailZReport = @Email_Z_Report, 
	shouldNotifyNoSale = @Notify_No_Sale, ScannerMode = @ScannerMode, ShouldEnableClockIn = @ShouldEnableClockIn, HoldAndFire = @HoldAndFire, CashDrawerBalancing = @CashDrawerBalancing, NoSaleLimit = @NoSaleLimit  
where Rest_Id = @Rest_ID

END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Restaurant_Info_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Restaurant_Info_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Restaurant_Info_Update]
@RestName VarChar(150) = null,
@Initials Char(5) = null,
@Address1 VarChar(255) = null,
@Address2 VarChar(255) = null,
@City VarChar(50) = null,
@State int = 0,
@Zip VarChar(50) = null,
@Phone VarChar(50) = null,
@Fax VarChar(50) = null,
@Email VarChar(255) = null,
@WebSite VarChar(100) = null,
@TablesCount int = 0,
--@Header VarChar(255) = null,
@Footer1 VarChar(1000) = null,
@Footer2 VarChar(1000) = null,
@KitchenView Bit = false,
@ExpediteView Bit =false,
@Tax Bit =false,
@Status int = 0,
@sDate DateTime,
@LoggedUserID int = 0,
@Mode VarChar(20) = null,
@RestID int = 0,

@Header_Name varchar(255) = null,
@Header_Address1 VarChar(1000) = null,
@Header_City VarChar(50) = null,
@Header_State VarChar(50) = null,
@Header_Zip VarChar(20) = null,
@Header_Phone VarChar(20) = null,
@Header_ABN VarChar(25) = null,
@Header_TaxInvoice Bit =false,
@Header_Website VarChar(100) = null,
@Header_Email VarChar(100) = null


AS

IF @Mode=''add''

BEGIN

insert into omni_Restuarnt_info(RestName, Initials, Address1, Address2, City,
StateID, Zip, Phone, Fax, Email, Website, TablesCount, Footer1, Footer2, KitchenView,
ExpediteView, Tax, CreatedOn, CreatedBy, IsActive,
Header_Name, Header_Address1, Header_City, Header_State, Header_Zip,
Header_Phone, Header_ABN, Header_TaxInvoice, Header_Website, Header_Email)

 values(@RestName, @Initials, @Address1, @Address2, @City, @State,
 @Zip, @Phone, @Fax, @Email, @WebSite, @TablesCount, @Footer1, @Footer2, @KitchenView,
 @ExpediteView, @Tax, @sDate, @LoggedUserID, @Status,
@Header_Name, @Header_Address1, @Header_City, @Header_State, @Header_Zip,
@Header_Phone, @Header_ABN, @Header_TaxInvoice, @Header_Website, @Header_Email)
  
END

IF @Mode=''edit''

BEGIN

update omni_Restuarnt_info set RestName = @RestName, Initials = @Initials, Address1 = @Address1, 
Address2 = @Address2 , City = @City , StateID = @State, Zip = @Zip, Phone = @Phone , Fax =@Fax, Email =@Email, Website=@WebSite ,TablesCount = @TablesCount, Footer1 =@Footer1, Footer2 =@Footer2, 
KitchenView = @KitchenView, ExpediteView = @ExpediteView, Tax=@Tax,  ModifiedOn = @sDate, 
ModifiedBy = @LoggedUserID, IsActive =@Status ,
Header_Name = @Header_Name, Header_Address1 =  @Header_Address1, Header_City =  @Header_City, 
Header_State = @Header_State, Header_Zip =  @Header_Zip,
Header_Phone = @Header_Phone , Header_ABN= @Header_ABN, 
Header_TaxInvoice = @Header_TaxInvoice, Header_Website =  @Header_Website, Header_Email= @Header_Email

where Rest_Id = @RestID

END






' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Refund_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Refund_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_Refund_Update]
@RefundTranID	VarChar(50) = null ,
@RefundDate		DateTime,
@ProductID		int = 0,
@DeviceID		int = 0,
@Comments		VarChar(5000) = null,
@Rest_ID		int = 0,
@PaymentTypeID	int = 0,
@Amount			Decimal,
@CreatedByUserID int=0,
@CreatedOn		DateTime,
@Mode			VarChar(20)=null

AS

IF @Mode=''add''

BEGIN

insert into omni_Refund(TransactionID,RefundDate,ProductID,DeviceID,Comments,Rest_ID,
PaymentTypeID,Amount,CreatedByUserID, CreatedOn)
 values(@RefundTranID,@RefundDate,@ProductID, @DeviceID, @Comments,@Rest_ID,
 @PaymentTypeID, @Amount, @CreatedByUserID,@CreatedOn)
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Product_Update]
@ProductID				int = 0,
@ProductName			varchar(50) = null,
@ProductName2			varchar(50) = null,
@ProductDesc			varchar(3000) = null,
@ProductColor			char(10)=null,
@BarCode				varchar(50)=null,
@GST					bit = false,
@HasOpenPrice			bit = false,
@HasFlag				bit = false,
@IsDailyItem			bit =false,
@IsSoleProduct			bit =true,
@ProductPrice1			decimal(7,2) = 0.00,
@ProductPrice2			decimal(7,2) = 0.00,
@ProductPrice3			decimal(7,2) = 0.00,
@ProductPrice4			decimal(7,2) = 0.00,
@ProductPrice5			decimal(7,2) = 0.00,
@OpQty					decimal(7,2) = 0.00,
@StockInHand			decimal(7,2) = 0.00,
@ProductImageWithPath	varchar(255) = null, 
@LoggedUserID			int = 0,
@RestID					int = 0,
@sDate					datetime,
@Status					int = 0,
@Mode  					nvarchar(20)=null,
@CategoryID				int = 0,
@ChangePrice			bit = false,
@ReOrdLvl				decimal(7,2) = 0,
@ReOrdQty				decimal(7,2) = 0,
@SortOrder				int = 0,
@CourseID				int = 0,
@SupplierID				int = 0,
@UnitID					int = 0,
@Points					decimal(7,2) = 0.00
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN
declare @cnt int

set @cnt = (select COUNT(*) from omni_Products where ProductName=@ProductName and Rest_ID=@RestID)

if (@cnt=0 and @ProductName !='''') 

insert into omni_Products(CategoryID,ProductName, ProductName2, ProductDescription, Color,BarCode,
GST, HasOpenPrice, IsDailyItem,IsSoleProduct, HasFlag, Price1, Price2,Price3, Price4, Price5, OpQty, StockInHand, ProductImageWithPath,
Rest_ID, CreatedByUserID, CreatedOn, IsActive, ChangePrice, SortOrder, CourseID,SupplierID, UnitID,ReOrderLevel, 
ReOrderQty, Points)
 values(@CategoryID,@ProductName, @ProductName2, @ProductDesc, @ProductColor,@BarCode,
 @GST, @HasOpenPrice,@IsDailyItem ,@IsSoleProduct ,@HasFlag, @ProductPrice1,@ProductPrice2,@ProductPrice3,@ProductPrice4,@ProductPrice5, @OpQty, @StockInHand, @ProductImageWithPath,
 @RestID, @LoggedUserID, @sDate, @Status, @ChangePrice, @SortOrder, @CourseID,@SupplierID,@UnitID, @ReOrdLvl,@ReOrdQty, @Points)
END

IF @Mode=''edit''

BEGIN
	update omni_Products set CategoryID = @CategoryID, ProductName = @ProductName, ProductName2 = @ProductName2, ProductDescription = @ProductDesc,
	Color =@ProductColor, BarCode = @BarCode, GST = @GST, HasFlag = @HasFlag, HasOpenPrice = @HasOpenPrice, IsDailyItem = @IsDailyItem,IsSoleProduct = @IsSoleProduct ,Price1 = @ProductPrice1, Price2 = @ProductPrice2,
	Price3 = @ProductPrice3, Price4 = @ProductPrice4, Price5 = @ProductPrice5, OpQty = @OpQty, StockInHand = @StockInHand, ProductImageWithPath = @ProductImageWithPath,	
	ModifiedByUserID = @LoggedUserID, ModifiedOn = @sDate, IsActive= @Status, ChangePrice = @ChangePrice, SortOrder= @SortOrder , CourseID = @CourseID, 
	SupplierID = @SupplierID,UnitID = @UnitID,  ReOrderLevel = @ReOrdLvl, ReOrderQty = @ReOrdQty, Points = @Points 
	where ProductID = @ProductID
END


IF @Mode=''del''

BEGIN

delete from omni_Products where ProductID=@ProductID and Rest_ID=@RestID
end


' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Printer_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Printer_Options_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Product_Printer_Options_Update]
--@KitchenID								int = 0,
@PrinterID								int = 0,
@ProductID								int = 0,
@RestID									int = 0
--@Mode  								varchar(10)=null
AS

--IF @Mode=''add''

BEGIN

insert into omni_Product_Kitchen_Printer_Options(ProductID, OptionID ,OptionType,Rest_ID)
 values(@ProductID,@PrinterID,''P'',@RestID)

END

--IF @Mode=''edit''

--BEGIN
--declare @cnt int=0

--set @cnt = (select COUNT(*) as totcnt from omni_Product_Kitchen_Printer_Options where ProductID=@ProductID and OptionType = ''P'')

--if (@cnt>0)

--update omni_Product_Kitchen_Printer_Options set OptionID=@PrinterID 
--where ProductID = @ProductID and OptionType = ''P''

--else
--insert into omni_Product_Kitchen_Printer_Options(ProductID, OptionID ,OptionType)
-- values(@ProductID,@PrinterID,''P'')

--END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Options_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Product_Options_Update]
@Product_ModifierID					int = 0,
@ModifierID							int = 0,
@ProductID							int = 0,
@RestID								int = 0,
@Mode  								nvarchar(20)=null
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_Product_Modifiers(ModifierID,ProductID,Rest_ID)
 values(@ModifierID,@ProductID,@RestID)
END

IF @Mode=''edit''

BEGIN
delete from omni_Product_Modifiers where ProductID = @ProductID and ModifierID=@ModifierID

insert into omni_Product_Modifiers(ModifierID,ProductID,Rest_ID)
 values(@ModifierID,@ProductID,@RestID)

END

IF @Mode=''del''

BEGIN
delete from omni_Product_Modifiers where ModifierID=@ModifierID 

END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Mixing_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Mixing_Options_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_Product_Mixing_Options_Update]
@ProductID								int = 0,
@IngID									int = 0,
@MixingType							char(25) = null,
@RestID									int = 0,
@Mode  								nvarchar(20)=null
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_Product_Ingredient_SubreciepeDetails(ProductID,IngredientId,Rest_Id,MixingType)
 values(@ProductID,@IngID,@RestID,@MixingType)
END

IF @Mode=''edit''

BEGIN
delete from omni_Product_Ingredient_SubreciepeDetails where ProductID = @ProductID  and IngredientId = @IngID and MixingType = @MixingType and Rest_Id=@RestID

insert into omni_Product_Ingredient_SubreciepeDetails(ProductID,IngredientId,Rest_Id,MixingType)
 values(@ProductID,@IngID,@RestID,@MixingType)

END

IF @Mode=''del''

BEGIN
delete from omni_Product_Ingredient_SubreciepeDetails where ProductID = @ProductID and IngredientId=@IngID and Rest_Id = @RestID

END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Kitchen_Printer_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Kitchen_Printer_Options_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_omni_Product_Kitchen_Printer_Options_Update]
@KitchenID								int = 0,
@PrinterID								int = 0,
@ProductID								int = 0,
@Mode  								varchar(10)=null,
@cnt int=0
AS

IF @Mode=''add''

BEGIN

insert into omni_Product_Kitchen_Printer_Options(ProductID, OptionID ,OptionType)
 values(@ProductID,@KitchenID,''K'')
 
insert into omni_Product_Kitchen_Printer_Options(ProductID, OptionID ,OptionType)
 values(@ProductID,@PrinterID,''P'')

END

IF @Mode=''edit''

BEGIN


set @cnt = (select COUNT(*) as totcnt from omni_Product_Kitchen_Printer_Options where ProductID=@ProductID and OptionType = ''K'')

if (@cnt>0)
update omni_Product_Kitchen_Printer_Options set OptionID=@KitchenID 
where ProductID = @ProductID and OptionType = ''K''
else
insert into omni_Product_Kitchen_Printer_Options(ProductID, OptionID ,OptionType)
 values(@ProductID,@KitchenID,''K'')


set @cnt = (select COUNT(*) as totcnt from omni_Product_Kitchen_Printer_Options where ProductID=@ProductID and OptionType = ''P'')

if (@cnt>0)

update omni_Product_Kitchen_Printer_Options set OptionID=@PrinterID 
where ProductID = @ProductID and OptionType = ''P''

else
insert into omni_Product_Kitchen_Printer_Options(ProductID, OptionID ,OptionType)
 values(@ProductID,@PrinterID,''P'')

END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Kitchen_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Kitchen_Options_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Product_Kitchen_Options_Update]
@KitchenID								int = 0,
@ProductID								int = 0,
@RestID									int = 0,
@Mode  								varchar(10)=null
AS

iF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_Product_Kitchen_Printer_Options(ProductID, OptionID ,OptionType,Rest_ID)
 values(@ProductID,@KitchenID,''K'',@RestID)

END

IF @Mode=''edit''

BEGIN
update omni_Product_Kitchen_Printer_Options set OptionID=@KitchenID ,Rest_ID=@RestID
where ProductID = @ProductID and OptionType = ''K''

END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Cooking_Options_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Cooking_Options_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Product_Cooking_Options_Update]
@OptionID								int = 0,
@ProductID								int = 0,
@RestID									int = 0,
@Mode  								nvarchar(20)=null
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_Product_Cooking_Options(OptionID,ProductID,Rest_ID)
 values(@OptionID,@ProductID,@RestID)
END

IF @Mode=''edit''

BEGIN
delete from omni_Product_Cooking_Options where ProductID = @ProductID and OptionID=@OptionID

insert into omni_Product_Cooking_Options(OptionID,ProductID,Rest_ID)
 values(@OptionID,@ProductID,@RestID)

END

IF @Mode=''del''

BEGIN
delete from omni_Product_Cooking_Options where OptionID=@OptionID

END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Product_Clone]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Product_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Product_Clone]
@SourceRestID	int = 0,
@TargetRestID	int = 0,
@CreatedByUserID int=0,
@CreatedOn		DateTime

AS


BEGIN

declare @cnt int

set @cnt = (select count(*) as totcnt from omni_Products where Rest_ID=@TargetRestID and ProductName in (select Productname from omni_Products where Rest_ID=@SourceRestID))

if (@cnt=0)  

  insert into omni_Products(
  ProductName,ProductName2,ProductDescription,Color,GST,HasOpenPrice,ChangePrice,Price1,Price2,StockInHand,SortOrder,CategoryID,ProductImageWithPath,CourseID,Points,Rest_ID,CreatedByUserID,CreatedOn,IsActive) select 
  ProductName,ProductName2,ProductDescription,Color,GST,HasOpenPrice,ChangePrice,Price1,Price2,StockInHand,a.SortOrder,(select CategoryID from omni_item_categories where CategoryName=b.categoryname and Rest_id=@TargetRestID ) as CategoryID  ,ProductImageWithPath,( select courseid from omni_Courses where CourseName=c.coursename and Rest_id=@TargetRestID ) as CourseID,Points,@TargetRestID as Rest_ID,@CreatedByUserID as CreatedByUserID,@CreatedOn as CreatedOn,a.IsActive from omni_Products a 
  inner join omni_Item_Categories b on a.CategoryID=b.CategoryId 
  left join omni_Courses c on a.CourseID = c.CourseID
  where a.Rest_ID=@SourceRestID
  
 if (@cnt=0)  
  insert into omni_Product_Modifiers ( 
  ModifierID,ProductID,Rest_ID) select 
  (select ModifierID from omni_Modifiers where ModifierName=c.ModifierName and Rest_ID=@TargetRestID) as ModifierID , (select ProductID from omni_Products where productname= b.ProductName and Rest_ID=@TargetRestID) as ProductID, @TargetRestID as Rest_ID from omni_Product_Modifiers  a inner join 
omni_Products b on  a.ProductID = b.ProductID 
left join omni_Modifiers c on a.ModifierID=c.ModifierId 
where a.Rest_ID = @SourceRestID

if (@cnt=0)  
 insert into omni_Product_Cooking_Options (
  ProductID, OptionID, Rest_ID) select 
  (select ProductID from omni_Products where productname= b.ProductName and Rest_ID=@TargetRestID) as ProductID, 
  (select OptionID from omni_Cooking_Options where OptionName=c.OptionName and Rest_ID=@TargetRestID) as OptionID, @TargetRestID as Rest_ID from omni_Product_Cooking_Options a inner join 
omni_Products b on  a.ProductID = b.ProductID 
left join omni_Cooking_Options c on a.OptionID = c.OptionID 
  where a.Rest_ID = @SourceRestID

 if (@cnt=0)  
 Insert into omni_Product_Kitchen_Printer_Options ( 
 ProductID, OptionID, OptionType, Rest_ID ) select 
 (select ProductID from omni_Products where productname= b.ProductName and Rest_ID=@TargetRestID) as ProductID,
 (select OptionID from (select PrinterID as OptionID, PrinterName as OptionName,Rest_ID,''P'' as OptionType from omni_Printers 
 union select KitchenId as OptionID, KitchenName as OptionName,Rest_ID,''K'' as OptionType from omni_Kitchen ) as Q where Q.OptionName = c.OptionName and Rest_ID = @TargetRestID ) as OptionID,c.OptionType, @TargetRestID as Rest_ID from omni_Product_Kitchen_Printer_Options a 
  left join (select PrinterID as OptionID, PrinterName as OptionName,Rest_ID,''P'' as OptionType from omni_Printers 
 union select KitchenId as OptionID, KitchenName as OptionName,Rest_ID,''K'' as OptionType from omni_Kitchen ) as c on (a.OptionID = c.OptionID  and a.OptionType = c.OptionType) inner join 
 omni_Products b on  a.ProductID = b.ProductID 
 where a.Rest_ID=@SourceRestID

  
END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Printer_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Printer_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Printer_Update]
@PrinterID			int = 0,
@PrinterName		varchar(50) = null,
@IPAddress			varchar(50) = null,
@PosOrItem			char(1) = null,
@IsPrintIPAddress	char(1) = null,
@Rest_ID			int = 0,
@LoggedUserID		int = 0,
@sDate				datetime,
--@ModifyDate		datetime = ''01/01/1900'',
--@DeleteDate		datetime = ''01/01/1900'',
@Status			int = 0,
@Mode  			nvarchar(20)=null,
@PrinterType	char(1),
@NoOfCopies		int = 0,
@Trigger_Cash_Drawer	tinyint = 0
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_Printers(PrinterName,IPAddress, PosorItem, Rest_ID,
 IsPrintIpAddress, CreatedByUserID, CreatedOn,IsActive, PrinterType, NoOfCopies, Trigger_Cash_Drawer)
 values(@PrinterName,@IPAddress, @PosOrItem, @Rest_ID, 
 @IsPrintIPAddress, @LoggedUserID, @sDate,@Status, @PrinterType, @NoOfCopies, @Trigger_Cash_Drawer)
END

IF @Mode=''edit''

BEGIN

update omni_Printers set PrinterName =@PrinterName,
IPAddress = @IPAddress, PosorItem = @PosOrItem, Rest_id = @Rest_ID,
IsPrintIpAddress = @IsPrintIPAddress, 
ModifiedByUserID=@LoggedUserID, ModifiedDate = @sDate,IsActive =@Status, PrinterType=@PrinterType,
NoOfCopies=@NoOfCopies, Trigger_Cash_Drawer = @Trigger_Cash_Drawer  where PrinterId=@PrinterId
END

IF @Mode=''del''

BEGIN

delete from omni_Printers  where PrinterId=@PrinterId and Rest_id=@Rest_ID
END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Printer_Clone]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Printer_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_Printer_Clone]
@SourceRestID	int = 0,
@TargetRestID	int = 0,
@CreatedByUserID int=0,
@CreatedOn		DateTime

AS

BEGIN

declare @cnt int

set @cnt = (select count(*) as totcnt from omni_Printers where Rest_ID=@TargetRestID and PrinterName in (select PrinterName from omni_Printers where Rest_ID=@SourceRestID))

if (@cnt=0)  

  insert into omni_Printers(
  PrinterName, IPAddress, Rest_ID, PrinterType, PosorItem, NoOfCopies, Trigger_Cash_Drawer, IsPrintIpAddress, CreatedByUserID,CreatedOn, IsActive) select 
  PrinterName, IPAddress, @TargetRestID as Rest_ID, PrinterType, PosorItem, NoOfCopies, Trigger_Cash_Drawer, IsPrintIpAddress, @CreatedByUserID as CreatedByUserID,@CreatedOn as CreatedOn, IsActive from omni_Printers where Rest_ID=@SourceRestID
 
  
END





' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Payout_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Payout_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_Payout_Update]
@PayoutTranID	VarChar(50) = null ,
@PayoutDate		DateTime,
@DeviceID		int = 0,
@Comments		VarChar(5000) = null,
@Rest_ID		int = 0,
@PaymentTypeID	int = 0,
@Amount			Decimal,
@CreatedByUserID int=0,
@CreatedOn		DateTime,
@Mode			VarChar(20)=null

AS

IF @Mode=''add''

BEGIN

insert into omni_Payout(TransactionID,TransactionDate,DeviceID,Comments,Rest_ID,
PaymentTypeID,Amount,CreatedByUserID, CreatedOn)
 values(@PayoutTranID,@PayoutDate, @DeviceID, @Comments,@Rest_ID,
 @PaymentTypeID, @Amount, @CreatedByUserID,@CreatedOn)
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_OrderTransaction_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_OrderTransaction_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Kumar Sharma>
-- Create date: <25-09-2013>
-- Description:	<Updates order transaction by its amount and tax>
-- =============================================
CREATE PROCEDURE [dbo].[SP_omni_OrderTransaction_Update] 
	@OrderTranID		VarChar(50)=null,
	@Rest_id			int = 0,
	@GrossAmount		Decimal(18,2) = 0.00,
	@TotalTax			Decimal(8,2) = 0.00,
	@TotalAmount		Decimal(18,2) = 0.00,
	@OrderPaymentId		int = 0
AS
BEGIN
	
	update omni_OrderInfo set GrossAmount =@GrossAmount, TotalTax =@TotalTax, TotalAmount=@TotalAmount where Order_TranID=@OrderTranID and Rest_id=@Rest_id

END

BEGIN

	update omni_OrderPaymentInfo set PaidAmount =@GrossAmount where Order_TranID=@OrderTranID and OrderPaymentId=@OrderPaymentId

END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_OrderNote]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_OrderNote]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_OrderNote]
@NoteID	int = 0,
@Message		text = null,
@Rest_ID		int = 0,
@LoggedUserID	int = 0,
@sDate			datetime,
@Status		int = 0,
@Mode  			nvarchar(20)=null
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_Order_Note([Message], Rest_ID, CreatedByUserID, CreatedOn, IsActive )
 values(@Message, @Rest_ID,  @LoggedUserID, @sDate, @Status	)
END

IF @Mode=''edit''

BEGIN

update omni_Order_Note set [Message] =@Message,Rest_id = @Rest_ID,ModifiedByUserID=@LoggedUserID, ModifiedOn = @sDate , IsActive = @Status
 where NoteID = @NoteID

END

IF @Mode=''del''

BEGIN

delete from omni_Order_Note where NoteID = @NoteID and Rest_ID = @Rest_ID

END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_OrderInfo_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_OrderInfo_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_OrderInfo_Update]
@OrderTranID		VarChar(50)=null,
@OrderID			VarChar(50)=null,
@TransactionDate	DateTime,
@OrderedOn			DateTime,
@TableId			varchar(50) = null,
@NoOfGuest			int = 0,
@Table_OpenedOn		DateTime,
@Table_ClosedOn		DateTime,
--@SessionId			VarChar(255) = null,
@Rest_id			int = 0,
@UserID				int = 0,
@DeviceID			int = 0,
@GrossAmount		Decimal(18,2) = 0.00,
@TotalTax			Decimal(8,2) = 0.00,
@TipAmount			Decimal(8,2) = 0.00,
@Discount			Decimal(8,2) = 0.00,
@Surcharge			Decimal(8,2) = 0.00,
@TotalAmount		Decimal(18,2) = 0.00,
@PaymentType		int = 0,
@CreatedByUserID	int = 0,
@CreatedOn				DateTime,
@isPaid				Bit,
@Mode				VarChar(20),
@CustomerID		    int = 0


AS

IF @Mode=''add''

BEGIN
--declare @cnt int
--set @cnt = (select COUNT(*) from omni_OrderInfo where OrderID=@OrderID and Rest_ID=@Rest_id)

--if (@cnt=0 and @OrderID !='''') 

insert into omni_OrderInfo(Order_TranID,OrderNo,TransactionDate,OrderedOn,TableId,NoOfGuest,
Table_OpenedOn,Table_ClosedOn, Rest_id, UserID, DeviceID,
GrossAmount, TotalTax, TipAmount, Discount, Surcharge,TotalAmount, CreatedByUserID, CreatedOn, isPaid, CustomerID)
 values(@OrderTranID,@OrderID,@TransactionDate, @OrderedOn, @TableId,@NoOfGuest,
 @Table_OpenedOn, @Table_ClosedOn, @Rest_id, @UserID, @DeviceID,
 @GrossAmount, @TotalTax, @TipAmount, @Discount, @Surcharge, @TotalAmount, @UserID,@CreatedOn,@isPaid,@CustomerID)
END

IF @CustomerID > 0
BEGIN
update omni_Customers set LastOrderID=@OrderTranID where CustomerId = @CustomerID
END





' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Order_ProductInfo_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Order_ProductInfo_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_omni_Order_ProductInfo_Update]
@OrderTranID			VarChar(50)=null,
@ProductID			int = 0,
@Qty				decimal(5,2) = 0.00,
@Amount			    decimal(18,2),
@modifiers		varchar(50) = null
AS

BEGIN

insert into omni_Order_ProductDetails(Order_TranID,ProductID,Qty,Amount,ModifierIDS)
 values(@OrderTranID,@ProductID,@Qty,@Amount,@modifiers)
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Order_Product_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Order_Product_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Kumar Sharma>
-- Create date: <24-09-2013>
-- Description:	<Update omni product order details>
-- =============================================
CREATE PROCEDURE [dbo].[SP_omni_Order_Product_Update] 
	@OrderTranID		VarChar(50)=null,
	@ProductID			int = 0,
	@Qty				decimal(5,2) = 0.00,
	@Amount			    decimal(18,2)
AS
BEGIN
	update omni_Order_ProductDetails set Qty =@Qty, Amount=@Amount where Order_TranID=@OrderTranID and ProductID=@ProductID
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Order_PaymentInfo_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Order_PaymentInfo_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Order_PaymentInfo_Update]
@OrderTranID			VarChar(50)=null,
@PaymentTypeID		int = 0,
@PaidAmount			    decimal(18,2) = 0.00,
@TranDate			datetime

AS

BEGIN


insert into omni_OrderPaymentInfo(Order_TranID,PaymentOn,PaymentTypeID,PaidAmount)
 values(@OrderTranID,@TranDate,@PaymentTypeID,@PaidAmount)
END





' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Order_Adjustment_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Order_Adjustment_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Order_Adjustment_Update]
@FromDate		DateTime,
@TillDate		DateTime,
@Adjustment		Decimal(6,2),
@LoggedUserID	int,
@sDate			DateTime,
@Rest_ID		int =0
AS

BEGIN
declare @cnt int
set @cnt = (select COUNT(*) from omni_OrderInfo where TransactionDate>=@FromDate and TransactionDate <=@TillDate and Rest_id=@Rest_ID)

if (@cnt>0) 

update omni_OrderInfo set AdjustmentPercent= @Adjustment, ModifiedByUserID= @LoggedUserID, ModifiedOn=@sDate where TransactionDate>=@FromDate and TransactionDate<=@TillDate and  Rest_id=@Rest_ID

END







' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Order_Adjustment_Log_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Order_Adjustment_Log_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_Order_Adjustment_Log_Update]
@FromDate		DateTime,
@TillDate		DateTime,
@OrderTranID	Text,
@Rest_ID		int =0,
@LoggedUserID	int = 0,
@sDate		datetime
AS

BEGIN
insert into omni_Order_Adjustment_Log (FromDate, TillDate, OrderTranID, Rest_ID, CreatedByUserID, CreatedOn) 
values (@FromDate, @TillDate,@OrderTranID,@Rest_ID,@LoggedUserID,@sDate)

END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Option_Settings_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Option_Settings_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_Option_Settings_Update]
@OptionName			varchar(50)=null,
@OptionValue		varchar(50)=null

AS

BEGIN

declare @cnt int

set @cnt = (select COUNT(*) from omni_Option_Settings where OptionName=@OptionName)

if (@cnt = 0)

insert into omni_Option_Settings(OptionName, OptionValue) 
values (@OptionName,@OptionValue)

else

update omni_Option_Settings set OptionValue=@OptionValue where OptionName= @OptionName

END





' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_NoSale_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_NoSale_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROCEDURE [dbo].[SP_omni_NoSale_Update]
@TranID		VarChar(50) = null ,
@Date		DateTime,
@Rest_ID	int = 0,
@Count		int=0,
@DeviceID	int = 0,
@UserID		int=0,
@Note		VarChar(500) = null
AS

BEGIN

insert into omni_NoSale(TransID,Date,RestID,NoSaleCount,DeviceID,UserID,Note)
values(@TranID,@Date,@Rest_ID,@Count,@DeviceID,@UserID,@Note)

END






' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_ModifierLevel_Clone]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_ModifierLevel_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_ModifierLevel_Clone]
@SourceRestID	int = 0,
@TargetRestID	int = 0,
@CreatedByUserID int=0,
@CreatedOn		DateTime

AS

BEGIN

declare @cnt int

set @cnt = (select count(*) as totcnt from omni_Modifiers_Level where Rest_ID=@TargetRestID and ModifierLevelName in (select ModifierLevelName from omni_Modifiers_Level where Rest_ID=@SourceRestID))

if (@cnt=0)  

  insert into omni_Modifiers_Level(
  ModifierLevelName,Rest_ID,CreatedByUserID,CreatedOn,IsActive) select 
  ModifierLevelName,@TargetRestID as Rest_ID,@CreatedByUserID as CreatedByUserID,@CreatedOn as CreatedOn,IsActive from omni_Modifiers_Level where Rest_ID=@SourceRestID
 
  
END






' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Modifier_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Modifier_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Modifier_Update]
@ModifierID					int = 0,
@ModifierName				varchar(50) = null,
@Name2						varchar(50) = null,
@Price1						decimal(9,2) = 0.00,
@Price2						decimal(9,2) = 0.00,
@SortOrder					int = 0,
@ModifierLevelID			int = 0,
@RestID						int = 0,
@LoggedUserID int = 0,
@sDate		datetime,
--@ModifyDate		datetime = ''01/01/1900'',
--@DeleteDate		datetime = ''01/01/1900'',
@GST						bit,
@Status						int = 0,
@Mode  						nvarchar(20)=null
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN
declare @cnt int
set @cnt = (select COUNT(*) from omni_Modifiers where ModifierName=@ModifierName and Rest_ID=@RestID)

if (@cnt=0 and @ModifierName !='''') 

insert into omni_Modifiers(ModifierName,Name2,Price1,Price2,SortOrder,Rest_ID,GST ,ModifierLevelID,CreatedByUserID, CreatedOn,IsActive)
 values(@ModifierName, @Name2, @Price1,@Price2 ,@SortOrder,@RestID,@GST, @ModifierLevelID, @LoggedUserID, @sDate, @Status)
END

IF @Mode=''edit''

BEGIN

update omni_Modifiers set ModifierName = @ModifierName,Name2=@Name2, Price1=@Price1, Price2=@Price2, GST = @GST, SortOrder=@SortOrder,
ModifierLevelID = @ModifierLevelID, Rest_ID=@RestID, ModifiedByUserID=@LoggedUserID, ModifiedOn = @sDate,
IsActive =@Status where ModifierID = @ModifierID

END


IF @Mode=''del''

BEGIN

delete from omni_Modifiers where ModifierID = @ModifierID and Rest_ID=@RestID

END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Modifier_Clone]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Modifier_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_Modifier_Clone]
@SourceRestID	int = 0,
@TargetRestID	int = 0,
@CreatedByUserID int=0,
@CreatedOn		DateTime

AS

BEGIN

declare @cnt int

set @cnt = (select count(*) as totcnt from omni_Modifiers where Rest_ID=@TargetRestID and Modifiername in (select Modifiername from omni_Modifiers where Rest_ID=@SourceRestID))

if (@cnt=0)  

  insert into omni_Modifiers(
  ModifierName,Name2, Price1, Price2, [Description], ModifierLevelID, SortOrder, Rest_ID, CreatedByUserID, CreatedOn,IsActive) select 
  ModifierName,Name2, Price1, Price2, [Description], (select LevelID from omni_Modifiers_Level where ModifierLevelName = b.ModifierLevelName and Rest_ID=@TargetRestID) as ModifierLevelID,
SortOrder, @TargetRestID as  Rest_ID, @CreatedByUserID as CreatedByUserID, @CreatedOn as  CreatedOn,a.IsActive from omni_Modifiers a 
inner join omni_Modifiers_Level b on a.ModifierLevelID = b.LevelID where a.Rest_ID=@SourceRestID

  
END









' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_modifer_level_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_modifer_level_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_modifer_level_Update]
@LevelID					int = 0,
@ModifierLevelName			varchar(50) = null,
@LoggedUserID				int = 0,
@sDate						datetime,
--@ModifyDate		datetime = ''01/01/1900'',
--@DeleteDate		datetime = ''01/01/1900'',
@RestID						int = 0,
@Status						int = 0,
@Mode  						nvarchar(20)=null

AS

IF @Mode=''add'' or @Mode=''clone'' 

BEGIN
declare @cnt int
set @cnt = (select COUNT(*) from omni_Modifiers_Level where ModifierLevelName=@ModifierLevelName and Rest_ID=@RestID)

if (@cnt=0 and @ModifierLevelName !='''') 
 insert into omni_Modifiers_Level(ModifierLevelName,Rest_ID,CreatedByUserID, CreatedOn,IsActive)
 values(@ModifierLevelName,@RestID, @LoggedUserID, @sDate,@Status)
END

IF @Mode=''edit''

BEGIN

update omni_Modifiers_Level set ModifierLevelName = @ModifierLevelName,
ModifiedByUserID=@LoggedUserID, ModifiedOn = @sDate,IsActive =@Status where
LevelID = @LevelID
END

IF @Mode=''del''

BEGIN

delete from omni_Modifiers_Level where LevelID = @LevelID and Rest_ID=@RestID
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_LastUpdates]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_LastUpdates]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_omni_LastUpdates]
@Action				VarChar(50) =null,
@TransactDate		DateTime,
@Rest_ID			Int = 0

AS

BEGIN

declare @cnt int

--set @TransactDate = (select getdate())
--set @TransactDate = (select Convert(VARCHAR(10),GETDATE(),101) + '' '' + Convert(VARCHAR(8),GETDATE(),108))

set @cnt = (select COUNT(*) from omni_Last_Updates where ActionName=@Action and Rest_ID=@Rest_ID)

if (@cnt = 0)

insert into omni_Last_Updates(ActionName,TransactDate,Rest_ID) 
values (@Action,@TransactDate,@Rest_ID)

else

update omni_Last_Updates set TransactDate = @TransactDate, Rest_ID=@Rest_ID where ActionName = @Action and Rest_ID=@Rest_ID

if (@Rest_ID=0)
	insert into omni_TransactionLog([Description],TransactionDate,Rest_ID) values(''ZReport_Company'',@TransactDate,0)
else
	insert into omni_TransactionLog([Description],TransactionDate,Rest_ID) values(''ZReport'',@TransactDate,@Rest_ID)
 
END





' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_kitchen_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_kitchen_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_kitchen_Update]
@KitchenID			int = 0,
@KitchenName		varchar(50) = null,
@Rest_ID			int = 0,
@LoggedUserID		int = 0,
@sDate				datetime,
--@ModifyDate		datetime = ''01/01/1900'',
--@DeleteDate		datetime = ''01/01/1900'',
@Status			int = 0,
@Mode  			nvarchar(20)=null
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_Kitchen(KitchenName, Rest_ID,
 CreatedByUserID, CreatedOn,IsActive)
 values(@KitchenName, @Rest_ID, 
 @LoggedUserID, @sDate,@Status)
END

IF @Mode=''edit''

BEGIN

update omni_Kitchen set KitchenName =@KitchenName,
Rest_id = @Rest_ID,ModifiedByUserID=@LoggedUserID, ModifiedOn = @sDate,IsActive =@Status where 
KitchenId=@KitchenID
END

IF @Mode=''del''

BEGIN

delete from omni_Kitchen where KitchenId=@KitchenID and Rest_ID = @Rest_ID
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Kitchen_Instruction]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Kitchen_Instruction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Kitchen_Instruction]
@InstructionID	int = 0,
@Message		text = null,
@Rest_ID		int = 0,
@LoggedUserID	int = 0,
@sDate			datetime,
@Status		int = 0,
@Mode  			nvarchar(20)=null
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_Kitchen_Instruction([Message], Rest_ID,
 CreatedByUserID, CreatedOn,IsActive)
 values(@Message, @Rest_ID, 
 @LoggedUserID, @sDate, @Status)
END

IF @Mode=''edit''

BEGIN

update omni_Kitchen_Instruction set [Message] =@Message,
Rest_id = @Rest_ID,ModifiedByUserID=@LoggedUserID, ModifiedOn = @sDate , IsActive=@Status
where InstructionID = @InstructionID

END

IF @Mode=''del''

BEGIN

delete from omni_Kitchen_Instruction where InstructionID = @InstructionID and Rest_ID=@Rest_ID

END





' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Kitchen_Clone]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Kitchen_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_Kitchen_Clone]
@SourceRestID	int = 0,
@TargetRestID	int = 0,
@CreatedByUserID int=0,
@CreatedOn		DateTime

AS

BEGIN

declare @cnt int

set @cnt = (select count(*) as totcnt from omni_Kitchen where Rest_ID=@TargetRestID and Kitchenname in (select KitchenName from omni_Kitchen where Rest_ID=@SourceRestID))

if (@cnt=0)  

  insert into omni_Kitchen(
  Kitchenname,Rest_ID,CreatedByUserID,CreatedOn, SortOrder,IsActive) select 
  Kitchenname,@TargetRestID as Rest_ID,@CreatedByUserID as CreatedByUserID,@CreatedOn as CreatedOn, SortOrder,IsActive from omni_Kitchen where Rest_ID=@SourceRestID
 
  
END





' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Ingredient_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Ingredient_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Ingredient_Update]
@IngID int = 0,
@IngredientName VarChar(50)=null,
@Price Decimal(6,2) = 0.00,
@BarCode varchar(50) = null,
@OpQty Decimal(7,2) = 0.00,
@ReOrdQty Decimal(7,2) = 0.00,
@ReOrdLvl Decimal(7,2) = 0.00,
@UnitID int = 0 ,
@SupplierID int = 0 ,
@Status int = 0,
@IsDailyItem bit = 0,
@sDate datetime,
@Rest_ID int = 0,
@LoggedUserID int = 0,
@Mode nvarchar(20)=null
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_Items_Ingredients(IngredientName,Price,UnitID,BarCode,OpQty,ReOrderLevel , ReOrderQty ,SupplierID ,Rest_ID,CreatedByUserID, CreatedOn,IsActive,IsDailyItem)
 values(@IngredientName,@Price,@UnitID,@BarCode,@OpQty,@ReOrdLvl ,@ReOrdQty ,@SupplierID,@Rest_ID, @LoggedUserID, @sDate,@Status,@IsDailyItem)
END

IF @Mode=''edit''

BEGIN

update omni_Items_Ingredients set IngredientName = @IngredientName,
Price=@Price,UnitId = @UnitID,BarCode = @BarCode,OpQty = @OpQty,ReOrderLevel =@ReOrdLvl , ReOrderQty = @ReOrdQty ,SupplierID = @SupplierID,
ModifiedByUserID=@LoggedUserID, ModifiedOn=@sDate,IsActive =@Status, IsDailyItem = @IsDailyItem where 
IngredientId = @IngID and Rest_ID = @Rest_ID
END

IF @Mode=''del''

BEGIN

delete from  omni_Items_Ingredients  where IngredientId = @IngID

END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_GroupPermission_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_GroupPermission_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_GroupPermission_Update]
@UserGroupID		int = 0,
@ParentMenuID		int = 0,
@MenuID				int  = 0 ,
@sDate				datetime,
@LoggedUserID		int = 0,
@Mode				char(10)

AS
Declare @count int

--IF @Mode=''add''

BEGIN

set @count = (select COUNT(*) from omni_Group_Permissions where UserGroupID=@UserGroupID and MenuID=@ParentMenuID)

if (@count=0) 
insert into omni_Group_Permissions(UserGroupID,MenuID,CreatedOn,CreatedByUserID) 
values(@UserGroupID,@ParentMenuID,@sDate,@LoggedUserID) 

set @count = (select COUNT(*) from omni_Group_Permissions where UserGroupID=@UserGroupID and MenuID=@MenuID)

if (@count=0) 

insert into omni_Group_Permissions(UserGroupID,MenuID,CreatedOn,CreatedByUserID) 
values(@UserGroupID,@MenuID,@sDate,@LoggedUserID) 

set @count = (select COUNT(*) from omni_Group_Permissions where UserGroupID=@UserGroupID and MenuID=26)

if (@count=0) 
insert into omni_Group_Permissions(UserGroupID,MenuID,CreatedOn,CreatedByUserID) 
values(@UserGroupID,26,@sDate,@LoggedUserID) 
 
END

--IF @Mode=''edit''

--BEGIN

----Delete from omni_Group_Permissions where UserGroupID = @UserGroupID and MenuID = @MenuID

--set @count = (select COUNT(*) from omni_Group_Permissions where UserGroupID=@UserGroupID and MenuID=26)

--if (@count=0) 
--insert into omni_Group_Permissions(UserGroupID,MenuID,CreatedOn,CreatedByUserID) 
--values(@UserGroupID,26,@sDate,@LoggedUserID) 

--set @count = (select COUNT(*) from omni_Group_Permissions where UserGroupID=@UserGroupID and MenuID=@MenuID)

--if (@count=0) 
--insert into omni_Group_Permissions(UserGroupID,MenuID,ModifiedOn,ModifiedByUserID) 
--values(@UserGroupID,@MenuID,@sDate,@LoggedUserID) 


--END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_DeviceInfo_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_DeviceInfo_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_DeviceInfo_Update]
@DeviceID					int = 0,
@DeviceName					varchar(100) = null,
@PrinterID					int = 0,
@Rest_ID					int = 0,
@LoggedUserID int = 0,
@sDate		datetime,
--@ModifyDate		datetime = ''01/01/1900'',
--@DeleteDate		datetime = ''01/01/1900'',
@Status						int = 0,
@Mode  						nvarchar(20)=null

AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_Device(DeviceName,PrinterID,Rest_ID,CreatedByUserID, CreatedOn,IsActive)
 values(@DeviceName, @PrinterID,@Rest_ID, @LoggedUserID, @sDate, @Status)
END

IF @Mode=''edit''

BEGIN

update omni_Device set DeviceName= @DeviceName,PrinterID = @PrinterID,Rest_ID= @Rest_ID,ModifiedByUserID=@LoggedUserID, ModifiedOn = @sDate,
IsActive =@Status where DeviceID= @DeviceID

END

IF @Mode=''del''

BEGIN

delete from omni_Device where DeviceID= @DeviceID and Rest_ID=@Rest_ID

END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Customer_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Customer_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Customer_Update]
@CustID int  = 0,
@FirstName VarChar(50) = null,
@LastName VarChar(50) = null,
@Email VarChar(100) = null,
@Address1 VarChar(255) = null,
@Address2 VarChar(255) = null,
@Phone VarChar(25) = null,
@ZipCode VarChar(25) = null,
@Status int = 0,
@RestID int = 0,
@sDate DateTime,
@LoggedUserID int = 0,
@Mode VarChar(20) = null

AS

IF @Mode=''add''

BEGIN

insert into omni_Customers(FirstName, LastName, Email, Address1, Address2, Phone,ZipCode,CreatedOn, CreatedByUserID, IsActive)
 values(@FirstName, @LastName, @Email, @Address1, @Address2, @Phone,@ZipCode,@sDate, @LoggedUserID, @Status)
  
END

IF @Mode=''edit''

BEGIN

update omni_Customers set FirstName = @FirstName, LastName= @LastName, Email = @Email, Address1 = @Address1, Address2= @Address2,Phone = @Phone, ZipCode = @ZipCode,  ModifiedOn = @sDate, ModifiedByUserID = @LoggedUserID, IsActive =@Status where CustomerID = @CustId

END

IF @Mode=''del''

BEGIN

delete from omni_Customers where CustomerId = @CustID 

end






' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Course_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Course_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Course_Update]
@CourseID	int = 0,
@CourseName	varchar(100) = null,
@SortOrder	int = 0,
@Rest_ID	int = 0,
@LoggedUserID int = 0,
@sDate		datetime,
--@ModifyDate		datetime = ''01/01/1900'',
--@DeleteDate		datetime = ''01/01/1900'',
@Status		int = 0,
@Mode  		nvarchar(20)=null



AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_Courses(CourseName,SortOrder,Rest_ID , CreatedByUserID, CreatedOn,IsActive)
 values(@coursename, @SortOrder, @Rest_ID, @LoggedUserID, @sDate,@Status)
END

IF @Mode=''edit''

BEGIN

update omni_Courses set CourseName = @Coursename, SortOrder = @SortOrder ,Rest_ID = @Rest_ID,
ModifiedByUserID=@LoggedUserID, ModifiedOn = @sDate,IsActive =@Status where
CourseID=@CourseID
END

IF @Mode=''del''

BEGIN

delete from omni_Courses where CourseID=@CourseID and Rest_ID=@Rest_ID
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Course_Clone]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Course_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_Course_Clone]
@SourceRestID	int = 0,
@TargetRestID	int = 0,
@CreatedByUserID int=0,
@CreatedOn		DateTime

AS

BEGIN
declare @cnt int

set @cnt = (select count(*) as totcnt from omni_Courses where Rest_ID=@TargetRestID and CourseName in (select CourseName from omni_Courses where Rest_ID=@SourceRestID))

if (@cnt=0)  

  insert into omni_Courses(
  CourseName,SortOrder, Rest_ID,CreatedByUserID,CreatedOn, IsActive) select 
  CourseName,SortOrder, @TargetRestID as Rest_ID,@CreatedByUserID as CreatedByUserID,@CreatedOn as CreatedOn, IsActive from omni_Courses where Rest_ID=@SourceRestID
 
  
END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_CookingOption_Clone]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_CookingOption_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_CookingOption_Clone]
@SourceRestID	int = 0,
@TargetRestID	int = 0,
@CreatedByUserID int=0,
@CreatedOn		DateTime
AS

BEGIN
declare @cnt int

set @cnt = (select count(*) as totcnt from omni_Cooking_Options where Rest_ID=@TargetRestID and OptionName in (select OptionName from omni_Cooking_Options where Rest_ID=@SourceRestID))


if (@cnt=0)  
  insert into omni_Cooking_Options(
  OptionName,Rest_ID,CreatedByUserID,CreatedOn,IsActive) select 
  OptionName,@TargetRestID as Rest_ID,@CreatedByUserID as CreatedByUserID,@CreatedOn as CreatedOn,IsActive from omni_Cooking_Options where Rest_ID=@SourceRestID
  
   
END






' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Cooking_Option_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Cooking_Option_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_Cooking_Option_Update]
@OptionID			int = 0,
@OptionName			varchar(100) = null,
@Rest_ID			int = 0,
@LoggedUserID		int = 0,
@sDate				datetime,
--@ModifyDate		datetime = ''01/01/1900'',
--@DeleteDate		datetime = ''01/01/1900'',
@Status			int = 0,
@Mode  			nvarchar(20)=null
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN

insert into omni_Cooking_Options(OptionName, Rest_ID,
 CreatedByUserID, CreatedOn,IsActive)
 values(@OptionName, @Rest_ID, 
 @LoggedUserID, @sDate,@Status)
END

IF @Mode=''edit''

BEGIN

update omni_Cooking_Options set OptionName = @OptionName,
Rest_id = @Rest_ID,ModifiedByUserID=@LoggedUserID, ModifiedOn = @sDate,IsActive =@Status where 
OptionID = @OptionID
END


IF @Mode=''del''

BEGIN

delete from omni_Cooking_Options  where OptionID = @OptionID and Rest_ID=@Rest_ID
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Company_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Company_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_Company_Update]
@CompanyName		VarChar(50) =null,
@Address			VarChar(2000) =null,
@Email				VarChar(100) = null,
@Phone				VarChar(50) =null,
@Fax				VarChar(50) = null,
@ABNNo				VarChar(25) = null,
@Tax				Decimal(9,2) = 0.00,
@Currency			VarChar(5) =null,
@DateFormat			VarChar(20) = null,
@sDate				datetime,
@LoggedUserID		int = 0,
@Mode				char(5)

AS

IF @Mode=''add''

BEGIN

insert into omni_CompanyInfo(CompanyName,Address,Email,Phone,Fax, ABNNumber , GlobalTax,
CurrencySymbol,DateFormat, CreatedOn, CreatedByUserID) 
values(@CompanyName,@Address, @Email,@Phone,@Fax,@ABNNo,@Tax , 
@Currency, @DateFormat,@sDate,@LoggedUserID) 
 
END

IF @Mode=''edit''

BEGIN

update omni_CompanyInfo set CompanyName = @CompanyName, Address = @Address , Email = @Email,
Phone = @Phone, Fax = @Fax, ABNNumber=@ABNNo, GlobalTax = @Tax,
CurrencySymbol = @Currency, DateFormat = @DateFormat , ModifiedOn = @sDate, ModifiedByUserID = @LoggedUserID

END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_ClearOrderTrans]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_ClearOrderTrans]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create proc [dbo].[SP_omni_ClearOrderTrans] 
@Order_TranID varchar(50) = null,
@RestID int,
@Mode varchar(20)
AS
BEGIN

Declare @cnt int

SET @cnt = (SELECT COUNT(*) FROM omni_OrderInfo where Order_TranID=@Order_TranID and Rest_id=@RestID)

	if ( @cnt>0 and @Mode=''del'') 
		Begin
			delete from omni_Order_ProductDetails where Order_TranID=@Order_TranID	
			delete from omni_OrderPaymentInfo where Order_TranID=@Order_TranID	
			delete from omni_OrderInfo where Order_TranID=@Order_TranID	and Rest_id=@RestID
		End 
End



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Category_Clone]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Category_Clone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[SP_omni_Category_Clone]
@SourceRestID	int = 0,
@TargetRestID	int = 0,
@CreatedByUserID int=0,
@CreatedOn		DateTime

AS

BEGIN

declare @cnt int

set @cnt = (select count(*) as totcnt from omni_Item_Categories where Rest_ID=@TargetRestID and CategoryName in (select CategoryName from omni_Item_Categories where Rest_ID=@SourceRestID and ParentId=0))

if (@cnt=0)  
  insert into omni_Item_Categories(
  CategoryName,Name2, ParentID, Rest_ID,CreatedByUserID,CreateDate, SortOrder,IsActive) select 
  CategoryName,Name2, ParentID,@TargetRestID as Rest_ID,@CreatedByUserID as CreatedByUserID, @CreatedOn as CreateDate, SortOrder,IsActive from omni_Item_Categories where Rest_ID=@SourceRestID and ParentId=0
 
  
END





' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_cate_subcate_Update]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_cate_subcate_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_omni_cate_subcate_Update]
@CategoryId		int = 0,
@CategoryName	varchar(50) = null,
@ParentID		int = 0,
@Rest_ID		int = 0,
@LoggedUserID	int = 0,
@sDate			datetime,
--@ModifyDate		datetime = ''01/01/1900'',
--@DeleteDate		datetime = ''01/01/1900'',
@Status			int = 0,
@Mode  			nvarchar(20)=null,
@Name2			varchar(100)=null,
@SortOrder		int = 0
AS

IF @Mode=''add'' or @Mode=''clone''

BEGIN
declare @cnt int
set @cnt = (select COUNT(*) from omni_Item_Categories where CategoryName=@CategoryName and Rest_ID=@Rest_ID)

if (@cnt=0 and @CategoryName !='''') 
insert into omni_Item_Categories(CategoryName,ParentID, Rest_ID,
 CreatedByUserID, CreateDate,IsActive, Name2, SortOrder)
 values(@CategoryName,@ParentID,@Rest_ID, @LoggedUserID, @sDate,@Status, @Name2, @SortOrder)
END

IF @Mode=''edit''

BEGIN

update omni_Item_Categories set CategoryName = @CategoryName,
ParentID = @ParentID, Rest_ID = @Rest_ID, ModifiedByUserID=@LoggedUserID, ModifiedDate = @sDate,IsActive =@Status, Name2= @Name2, SortOrder=@SortOrder where
CategoryId=@CategoryId
END

IF @Mode=''del''

BEGIN

delete from omni_Item_Categories where CategoryId=@CategoryId and Rest_id=@Rest_ID
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_omni_Add_User_Attendance]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_omni_Add_User_Attendance]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



CREATE PROCEDURE [dbo].[SP_omni_Add_User_Attendance]
@TranID		VarChar(50) = null ,
@Date		DateTime ,
@Rest_ID	int = 0,
@LoginTime	DateTime = null,
@LogoutTime	DateTime = null,
@UserID		int=0,
@Status		VarChar(50) = "Login"
AS

IF @Status=''Login''

BEGIN

insert into omni_UserAttendance(TransID,Date,RestID,isClosed,LoginTime,UserID)
values(@TranID,@Date,@Rest_ID,0,@LoginTime,@UserID)

END

IF @Status=''Logout''

BEGIN
	update omni_UserAttendance set LogoutTime = @LogoutTime, isClosed=1 where UserID=@UserID and RestID=@Rest_ID and isClosed = 0
END







' 
END
GO
/****** Object:  StoredProcedure [dbo].[getUserGroupDetails]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getUserGroupDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getUserGroupDetails]
	@UserGroupID int,
	@UserGroupName varchar(50) OUTPUT,
	@Status int OUTPUT
AS
SELECT 	@UserGroupName = UserGroupName,	@Status = Isactive
FROM omni_user_group WHERE UserGroupID = @UserGroupID



' 
END
GO
/****** Object:  StoredProcedure [dbo].[getUserDetails]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getUserDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[getUserDetails]
	@UserID int,
	--@UserAlias varchar(50) OUTPUT,
	@FirstName varchar(50) OUTPUT,
	@LastName varchar(50) OUTPUT,
	@UserPhone VarChar(20) = null OUTPUT,
	@StartDate DateTime = null OUTPUT,
	
	@UserAlias varchar(50) OUTPUT,
	@UserName varchar(50) OUTPUT,
	@UserPassword  binary(50) OUTPUT,
	@UserEmail varchar(100) OUTPUT,
	@UserGroupID int OUTPUT,
	@Status int OUTPUT,
	@UserPin	int OUTPUT,
	@HourlyRate	decimal(7,2) = 0 OUTPUT
AS
SELECT @FirstName = FirstName, @LastName = LastName, @UserPhone=UserPhone, @StartDate = StartDate, @UserAlias = UserAlias , @UserName = UserName, @UserPassword = UserPassword, @HourlyRate = HourlyRate,  
@UserEmail = UserEmail,@Status = IsActive, @UserGroupID = UserGroupID, @UserPin = UserPin 
FROM omni_users WHERE UserID = @UserID




' 
END
GO
/****** Object:  StoredProcedure [dbo].[getUnitDetails]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getUnitDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getUnitDetails]
	@UnitId			int,
	@UnitName varchar(50) OUTPUT,
	@Status			 int OUTPUT
AS
SELECT 	@UnitName= UnitName, @Status = Isactive 
FROM omni_UnitMaster WHERE  UnitID =@UnitId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[getTaxInfo]    Script Date: 03/08/2014 22:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getTaxInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[getTaxInfo]
	@TaxInfoID			int,
	@Literal		varchar(100) OUTPUT,
	@TaxRate	    decimal(6,2) OUTPUT,	
	@Status int OUTPUT
AS
SELECT @Literal = TaxInfoLiteral, @TaxRate = TaxRate,  @Status = IsActive
FROM omni_TaxInfo WHERE TaxInfoID= @TaxInfoID




' 
END
GO
