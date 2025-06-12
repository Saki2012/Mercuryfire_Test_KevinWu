# Mercuryfire_Test_KevinWu

此專案為面試技術測驗實作，使用 `.NET Core WebAPI` + `SQL Server` 完成完整的 CRUD 機制。資料庫部分使用 Stored Procedure 與 JSON 資料進行互動，並搭配 log 記錄架構。

---

## ✅ 專案啟動與資料庫還原說明

### 🧩 環境需求
- .NET Core SDK 6.0+
- SQL Server 2019 或以上版本
- Visual Studio 2022（建議）

### ▶️ 專案啟動步驟
1. Clone 此 Repo 至本地端
2. 還原 NuGet 套件
3. 建立 DB 並還原備份：

   ```sql
   -- SQL Server Management Studio (SSMS)
   -- 建立空白 DB 並還原
   RESTORE DATABASE Mercuryfire_Test_KevinWu
   FROM DISK = '你的路徑\Mercuryfire_Test_KevinWu.bak'
   WITH MOVE 'Mercuryfire_Test_KevinWu' TO 'C:\你的資料庫資料夾\Mercuryfire_Test_KevinWu.mdf',
        MOVE 'Mercuryfire_Test_KevinWu_log' TO 'C:\你的資料庫資料夾\Mercuryfire_Test_KevinWu.ldf',
        REPLACE
   ```

4. 確認 `appsettings.json` 中 `ConnectionStrings` 指向正確的 SQL Server 實例
5. 執行專案 → 可透過 Swagger (`/swagger`) 測試所有 API

---

## 💡 專案設計概念簡述

- 採用 ASP.NET Core WebAPI，資料模型與業務邏輯分離
- 所有 DB 操作統一透過 Stored Procedure 進行，避免硬編碼
- 使用 JSON 傳遞資料結構給 SP，提升欄位擴充彈性
- 使用 `usp_AddLog` 統一記錄每一次執行操作，並帶入 `_InBox_GroupID` 與 `_InBox_ReadID` 以支援可追溯性

---

## 📌 補充與反思

### ✴️ 欠缺或可優化的部分：

1. **資料庫連線與 SQL 執行邏輯**  
   Controller 中 `SqlCommand` 重複性高，未封裝成共用服務，未來可抽出 `DbHelper` 類別提升可維護性與測試性。

2. **SQL 與業務邏輯劃分**  
   目前所有邏輯皆集中於 Stored Procedure，若需複雜邏輯，未來可考慮搭配 DDD/CQRS 架構分層處理，將驗證、過濾等邏輯從 DB 搬至後端應用層。

---

## 🙏 備註與致歉

很抱歉本次提交時間稍微延誤，於原定期限後約 **半小時內**完成所有實作與測試、備份，並整理提交內容，感謝您的理解與包容。

---

如有任何補充或建議，敬請不吝指教，謝謝！
