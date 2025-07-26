# **Glossary**

## **Document**

A set of data representing an instance within the system. A **Document** includes:

1. **Metadata**, which specifies:
   - The associated **Formular** (defining its layout and structure).
   - The corresponding **Unit Map** (governing its journey through the system).
   - The current **Unit** (indicating its present stage in the workflow).
2. **Data**, which logically corresponds to the fields defined in the associated **Formular.**

The **Document** evolves as it traverses the workflow, and its metadata and field data may change depending on actions performed at each **Unit.**

---

## **Formular**

Equivalent to a "template." Represents the predefined structure, similar to a printed form, that must be filled out. A **Formular** is tied to specific document classes and ensures consistency in capturing required data and information.

---

## **Handoff**

Equivalent to a "transition." Refers to the act of transferring a **Document** from one **Unit** to another, signifying the movement of responsibility. A **Handoff** is governed by the rules defined in the **Unit Map** and progresses the **Document** through its lifecycle.

---

## **Unit**

Equivalent to a "task." Refers to a discrete stage in the document life cycle, representing specific work performed by a professional or system automation. A **Unit** is a cohesive work segment with a defined purpose. It must be fully completed or revisited entirely if retrying is necessary. Each **Unit** operates independently within the framework of the **Unit Map.**

---

## **Unit Steps**

Equivalent to the "steps" or "actions" within a task. These are predefined granular operations that collectively fulfill the purpose of a **Unit.** **Unit Steps** must be executed in sequence to complete the work for that stage.

Predefined **Unit Steps** commonly include:

- **Push Parent Document:** Handoff the current document to the next stage in the **Unit Map.**
- **Rollback Parent Document:** Recall the document to the previous stage in the **Unit Map.**
- **Create Child Document:** Generate a related document based on current execution conditions and data in the associated **Formular** instance.
- **Push Child Document:** Handoff a child document to another unit.
- **Rollback Child Document:** Recall a previously pushed child document.
- **Delete Child Document:** Permanently remove a child document from the system.
- **Send Email Notification:** Send a notification email to specified recipients.

Additionally, there is a **Custom Action Step**, enabling advanced workflows. This step executes custom logic, such as calling an external API endpoint or invoking a class method from a DLL.

---

## **Unit Map**

Equivalent to "routing." Represents the structured pathway dictating how a **Document** is processed within the system. A **Unit Map** indicates the sequence of operations, the desks or officers involved, and the relationships between tasks. It is conceptually represented as a directed graph.

---

## Example Usage

- The **Document** for an invoice includes metadata specifying the associated **Formular**, the **Unit Map** for processing, and the current **Unit** handling approvals, as well as itemized data that corresponds to fields in the **Formular.**
- The **Formular** for a purchase request ensures all necessary fields, such as vendor information and order details, are captured effectively.
- The **Unit Map** for expense reimbursements highlights the sequence of tasks and units, from manager approval to finance processing.
- At the current **Unit**, the system executes **Unit Steps** like **Create Child Document** to generate a payment file and **Send Email Notification** to inform stakeholders of progress.
- Once the validation **Unit** is completed, the document moves through a **Handoff** to the accounting department for final review.
- Within a **Transit Node**, documents are actively reviewed or processed before transitioning to the next stage in the **Unit Map.**
