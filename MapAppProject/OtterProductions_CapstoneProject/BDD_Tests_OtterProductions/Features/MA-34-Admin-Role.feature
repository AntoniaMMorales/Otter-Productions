@Antonia

Feature: MA-34-Admin-Role

    Basic testing for the Admin Role and Dashboard feature


Scenario: Admin dashboard page contains a View Users button
    Given I am the admin
        And I login
    When I navigate to the "Admin Dashboard" page
    Then The page presents a View Users button
    

Scenario: Admin dashboard page contains a welcome message
    Given I am the admin
        And I login
    When I navigate to the "Admin Dashboard" page
    Then The page contains an explanation message


Scenario: Non-admin user cannot access admin dashboard
    Given I am a registered user
        And I login
    When I navigate to the "Admin Dashboard" page
    Then I am redirected to the "Admin Error" page

Scenario: Ability to log in as an admin and view the dashboard exists
    Given I am the admin
        And I login
    When I navigate to the "Admin Dashboard" page
    Then The page title contains "Admin Dashboard"


Scenario: Can log in as an admin
    Given I am the admin
        And I login
    When I navigate to the "Home" page
    Then I can see a message in the navbar that includes my email