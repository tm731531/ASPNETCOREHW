各位同學大家好：
本周的回家作業，希望大家練習 ASP․NET Core Web API 與 Entity Framework Core 的整合開發，請大家將寫好的作業上傳到 GitHub 並將專案網址分享到本貼文留言中即可。
作業內容如下：
請以 ContosoUniversity 資料庫為主要資料來源
須透過 DB First 流程建立 EF Core 實體資料模型
須對資料庫進行版控 (使用資料庫移轉方式)
須對每一個表格設計出完整的 CRUD 操作 APIs
針對 Departments 表格的 CUD 操作需用到預存程序
請在 CoursesController 中設計 vwCourseStudents 與 vwCourseStudentCount 檢視表的 API 輸出
請用 Raw SQL Query 的方式查詢 vwDepartmentCourseCount 檢視表的內容
請修改 Course, Department, Person 表格，新增 DateModified 欄位(datetime)，並且這三個表格的資料透過 Web API 更新時，都要自動更新該欄位為更新當下的時間 (請新增資料庫移轉紀錄)
請修改 Course, Department, Person 表格欄位，新增 IsDeleted 欄位 (bit)，且讓所有刪除這三個表格資料的 API 都不能真的刪除資料，而是標記刪除即可，標記刪除後，在 GET 資料的時候不能輸出該筆資料。(請新增資料庫移轉紀錄)
請同學在練習的時候，務必要記錄開發總時間，並在回覆交件時寫上你總共花多少時間寫作業！真的做不出來的功能就先跳過沒關係，盡力完成作業即可。有問題隨時可以留言提問！