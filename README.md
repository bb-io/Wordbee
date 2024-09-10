
# Blackbird.io Wordbee

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

Wordbee is a powerful, cloud-based translation management system (TMS) designed for enterprises, language service providers (LSPs), and freelancers. It offers a comprehensive set of tools for managing the entire translation and localization workflow, from project creation and assignment to translation, review, and delivery

## Connecting

1.  Navigate to Apps, and identify the **Wordbee** app. You can use search to find it.
2.  Click _Add Connection_.
3.  Name your connection for future reference e.g. 'My organization'.
4. In your Wordbee instance go to `My account` -> `API` to get all the API credentials.
5.  Fill the `URL` field with the API url of your instance.
6. Fill the `Account ID` field with the Account property value from the settings.
7. Fille the `API key` field with the API key of your instance.
8.  When you return to Blackbird, confirm that the connection has appeared and the status is _Connected_.

## Actions

### Documents 

-   **Confirm delivery** confirms delivery of a specific document.
-   **Download translated file** downloads a translated file from the workflow.
-   **Submit new file** uploads a new file to an existing project.

### Orders

-   **Create order** creates a new order.
-   **Delete order** deletes a specific order.
-   **Download order files** downloads all order files as a ZIP archive.
-   **Get order** returns details of a specific order.
-   **Search orders** returns a list of multiple orders using filter criteria.

### Jobs

-   **Get job** returns details of a specific job.

### Projects

-   **Get project** returns details of a specific project.
-   **Search projects** returns a list of all projects in the workspace using filter criteria.

## Events

-   **On order message** triggers when any message is added to the order.
-   **On order status changed** triggers when the status of any order changes.
-   **On project status changed** triggers when the status of any project changes.
-   **On document workflow finished** triggers when the workflow of a specific document is finished.
-   **On order created** triggers when a new order is added.
-   **On job status changed** triggers when the status of a specific job is changed.

## Missing features

Wordbee is a huge app with a lot of features. If any of these features are particularly interesting to you, let us know!

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
