# First Day
- Learn how to Design and Implement Inventory(2 hour)
  - วิธีการจัดการข้อมูลหรือการออกแบบ UI และเขียน Script
- Design Class and System for Inventory System (2 hour)
  - ออกแบบ class ที่จะต้องเกี่ยวข้องกับระบบ Inventory
- Implement Basic Inventory System (5 hour)
  - สร้าง Basic UI สำหรับแสดงผม Inventory
  - สามารถเก็บ List ของ Item และแสดงผลออกมาได้
  - รองรับ Input ต่างๆ เช่น Drag&Drop สำหรับจัดการ Item หรือกดเลือกช่อง Item เพื่อใช้งาน
- Implement Player (2 hour)
  - การเคลื่อนไหวของ Player
  - สร้างตัวละคร Player เป็นสื่เหลี่ยม


# Second Day
- Integrate Inventory System with Player (1.5 hour)
  - เช่นการเลือกช่องที่ InventoryBar จะแสดงรูปของ Item ที่อยู่นะช่องนั้น, การใช้งาน Item
  - สร้าง GameManager เพื่อเป็นตัวกลางในการเชื่อม Inventory System และ Player
- Implement & Test Crafting UI (2 hour)
  - ออกแบบ UI เบื้องต้น
  - แสดงข้อมูล ItemData ที่สามารถ craft ได้
- Refactor Inventory UI and System (3 hour)
  - ทำการแก้ไขและปรับปรุง Inventory System มาจากตอนที่ได้ Implement ครั้งแรกไม่ได้มีการแยกระหว่าง UI และ Data Logic จึงต้องสร้าง InventoryUI สำหรับการแสดงผลและ Inventory สำรหรับเก็บและจัดการข้อมูล Item
- Implement, Test & Integrate Crafting System with Inventory System (4 hour)
  - ทำการเชื่อมระบบ Crafting กับ Inventory เข้าด้วยกัน เช่นตอนกด craft ของใน Player's Inventory ที่ใช้ในการ craft ต้องลดลงตามจำนวนที่ได้ระบุไว้ และเพิ่มของที่ craft ได้
  - เมื่อกดใช้ Storage Chest จะวางกล่องลงที่ Player
  - สร้าง ChestUI เพื่อแสดงผลของที่อยู่ในกล่องนั้นๆ
  - ปรับแก้การแสดงผลต่างๆ เช่นกล่องต้องอยู่หลัง Player
- Implement, Test Time Hop System (1 hour)
  - สร้าง UI และ Logic
- Fix Left over หรือ bug ที่พึ่งเจอ (1 hour)
  - Bug ที่แก้คือเมื่อย้ายของลงไปที่ Storage Chest แล้วของไม่ได้ไปใส่ใน Inventory component ของ Storage Chest

# Design
System Design: https://drive.google.com/file/d/1rJAeOVaGRVxN0sLItI0_Y0veRcxJqglZ/view?usp=sharing

# Completed
- Inventory System
  - Inventory UI: สำหรับแสดง Item ที่อยู่ใน Inventory
  - Inventory Class: สำหรับเก็บข้อมูลของ Item
  - ItemData: สำหรับเก็บข้อมูลของ Item ที่เป็นพื้นฐาน
  - Item: เก็บ ItemData และ จำนวน
- Crafting System
  - CraftingManager: ใช้สำหรับเก็บ List ของ ItemData ที่สามารถ craft ได้
  - InformationPanel: สำหรับแสดงข้อมูล Item ที่ต้องการ craft
- Time Hop
  - TimeManager: สำหรับจัดการช่วงเวลา, วัน และวันที่ รวมเก็บตัว GameEvent เพื่อเป็นตัวกลางในการับ request และ Update Event
  - TimePanel: สำหรับแสดงผลและ format เวลาและวันที่
 
สาเหตุที่เลือกที่จะทำ Inventory System ก่อนเพราะตัวของ Inventory ระบบพื้นฐานที่ Crafting และ Combat System ใชัและมีความเกี่ยวข้องกัน ส่วน TimeHop System เป็นระบบเดียวที่ไม่ได้เกี่ยวข้องกับระบบอื่นจึงสามารถเอาไว้ทำระบบสุดท้ายได้
# Remained
- Combat System
- Refine Other Art and UI

# Challenge & Problem
ผมไม่ได้มีประสบการณ์การใช้ Unity มากทำให้ผมค่อยข้างใช้เวลาในการเรียน, ทำความเข้าใจการเขียน Script และออกแบบโครงสร้างเกมสำหรับ Unity ซึ่งทำให้ผมต้องมีการ Refactor บ่อยสาเหตุมาจาก 
- Design ระบบที่ไม่ดีพอและขาดความเข้าใจจึงทำให้ต้องแก้ Design บ่อย
- Prototype มาจากการทดลองระบบที่ Design ไว้รวมถึงการใช้ Feature และ Function ต่างๆของ Unity ได้ไม่เต็มร้อย จึงต้องทดลองหลายอย่าง
- Test Function ของระบบต่างๆ ว่าเป็นไปตามที่ได้ต้องการไหมหรือครบตาม Use Cases ที่ควรจะเป็น
