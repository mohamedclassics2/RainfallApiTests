Feature: Rainfall API Enhancement Testing

    Scenario: Get rainfall measurements for a station with limit parameter
        Given I have a valid API key
        When I call the API to get rainfall measurements for a station with a limit of 10
        Then I should receive exactly 10 rainfall measurements
        And each measurement should be within the last hour

    Scenario: Get rainfall measurements for a station on a specific date
        Given I have a valid API key
        When I call the API to get rainfall measurements for a station on 2024-03-09
        Then I should receive rainfall measurements for that date
        And the measurements should be sorted chronologically