# **Demo Scenario: Handling a Technical Incident Through the System**

## **Overview**

You will demonstrate the system by walking through a workflow designed to manage a **tech incident**. This will involve:

1. Creating a **Document** for a technical incident using a predefined **Formular**.
2. Processing the **Document** through its **Unit Map**, which includes Units for:
   - First-line support analysis.
   - Second-line support resolution.
3. Generating **child Documents** (development tasks) based on the incident's requirements.
4. Demonstrating **Unit Steps** like **Push**, **Create Child Document**, and **Rollback**.
5. Resolving both development tasks (child Documents) to close the incident.

---

## **Detailed Scenario Flow**

### **Step 1: Creating the Technical Incident Document**

- Start the demo by selecting the **Formular** for "Technical Incident Reporting" (e.g., "tech_incident_form").
- Using this **Formular**, create a **Document** representing a technical incident. Example fields to populate:
  - **Incident Title:** "Button Not Responding on Submit"
  - **Description:** "The submit button on the main page is unresponsive when clicked."
  - **Priority:** "High"
  - **Reported By:** "John Doe"
  - **Date Reported:** "2023-01-01"

#### Step 1 Key Features Highlighted

- The system's ability to manage structured incident reporting with the appropriate **Formular.**

---

### **Step 2: Demonstrating the Unit Map for Incident Resolution**

- Explain the **Unit Map** for tech incidents:
  - The incident first goes to a **Unit** for **First-Line Support** review.
  - If unresolved, it escalates to **Second-Line Support**.

### **Unit 1: First-Line Support Analysis**

- Show the active **Unit** for the created incident (**Transit Node: First-Line Support**).
- Simulate an engineer reviewing the file but failing to resolve it directly.
- Use the **Push Parent Document** step to escalate the incident to **Second-Line Support**.

#### Unit 1 Key Features Highlighted

- The concept of **Unit Maps** and **Handoffs** between **Units**.
- Sequential progression from **First-Line** to **Second-Line Support**.

---

### **Step 3: Creating Child Documents for Development Tasks**

- At the **Second-Line Support** **Unit**, the engineer determines that resolving the incident requires two development tasks:
  1. Fix the button by changing its color.
  2. Add Spanish translations for the interface.

- Using predefined **Unit Steps**, create two **Child Documents**:
  1. **Child Document 1:** Development Task: Change Button Color
     - **Formular:** "Development Task Form"
     - **Fields Include:**
       - **Task Title:** "Change Button Color"
       - **Details:** "The button color must be updated to improve visibility and address functional issues."
       - **Assigned To:** "Frontend Team"
       - **Priority:** "High"
  2. **Child Document 2:** Development Task: Introduce Spanish Translation
     - **Formular:** "Development Task Form"
     - **Fields Include:**
       - **Task Title:** "Spanish Translation"
       - **Details:** "Translate the UI text to Spanish for international users."
       - **Assigned To:** "Localization Team"
       - **Priority:** "Medium"

- Handoff each **Child Document** to the appropriate development team using **Push Child Document**.

#### Step 3 Key Features Highlighted

- The ability to generate **Child Documents** from a parent **Document**.
- Demonstrating different **Formulars** for parent and child entities.
- Use of **Unit Steps** like **Create Child Document** and **Push Child Document.**

---

### **Step 4: Demonstrating Parallel Processing for Development Tasks**

- Switch the focus to each **Child Document's** journey:
  1. For **Child Document 1 (Button Color Change)**:
     - Show how the task is reviewed by the frontend team and marked as completed using the **Unit Step: Mark Task Completed.**
  2. For **Child Document 2 (Spanish Translation)**:
     - Demonstrate the task's progression, showcasing how it could be rolled back if incomplete (use **Unit Step: Rollback Child Document** as an example), but ultimately mark it as complete after correction.

#### Step 4 Key Features Highlighted

- Parallel workflows for **Child Documents**, running independently of the parent.
- Use of **Rollback** when corrections are needed.
- Finalization of individual tasks before resolving the incident.

---

### **Step 5: Resolving the Technical Incident**

- Once both development tasks are completed (**Child Documents are marked complete**), return to the parent **Document** at **Second-Line Support Unit**.
- Use the **Push Parent Document** step to transition the **Document** to the final **Unit**: "Incident Resolved."
- Update the parent **Document's** status to "Resolved" and archive it.

#### Step 5 Key Features Highlighted

- Proper linkage back to the parent **Document** after child tasks are completed.
- Logical workflow closure through the **Unit Map**.
- Archiving resolved incidents for audit and history purposes.

---

## **Key Demo Takeaways**

1. **Multiple Formulars and Maps:**
   - Show how different types of incidents or tasks utilize distinct **Formulars** and progress through unique **Unit Maps.**

2. **Unit and Unit Steps:**
   - Highlight the clear segmentation of workflows into **Units** and the atomic actions (**Unit Steps**) within them.
   - Demonstrate practical examples like **Push Parent Document**, **Create Child Document**, **Rollback**, and **Send Email Notification.**

3. **Flexible Workflow and Handoffs:**
   - Emphasize the ability to transition smoothly between teams or stages using **Handoffs.**

4. **Building and Managing Hierarchical Documents:**
   - Showcase the parent-child relationship between **Documents** and how child workflows integrate into the parent workflow seamlessly.

5. **Demonstrating Real-World Use Cases:**
   - Use a relatable scenario (e.g., resolving a tech incident) to show how the system operates in practice.
