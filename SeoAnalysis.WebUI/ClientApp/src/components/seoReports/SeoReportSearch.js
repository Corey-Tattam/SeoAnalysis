import React, { Component } from 'react';
import { Alert, Button, Card, Col, Form, InputGroup } from 'react-bootstrap';
import { FaSearch } from 'react-icons/fa';

export class SeoReportSearch extends Component {

    // #region " - - - - - - Fields - - - - - - "

    static displayName = SeoReportSearch.name;

    static SEO_REPORTS_URL_PATH = "api/SeoReports";

    // #endregion Fields

    // #region " - - - - - - Life-Cycle Methods - - - - - - "

    constructor(props) {
        super(props);
        this.state = {
            errorResponse: null,
            keywords: null,
            loading: false,
            seoAnalysisReport: null,
            shouldUpdate: false,
            url: null
        };

        // Data Access Functions.
        this.fetchSeoReport = this.fetchSeoReport.bind(this);

        // Event Handlers.
        this.handleErrorAlertClose = this.handleErrorAlertClose.bind(this);
        this.handleKeywordsInputChange = this.handleKeywordsInputChange.bind(this);
        this.handleUrlInputChange = this.handleUrlInputChange.bind(this);
        this.handleSearchButtonClick = this.handleSearchButtonClick.bind(this);

        // Render Functions.
        this.renderErrorAlert = this.renderErrorAlert.bind(this);
        this.renderResults = this.renderResults.bind(this);
        this.renderSearchFields = this.renderSearchFields.bind(this);
    };

    shouldComponentUpdate(nextProps, nextState) {
        return nextState.shouldUpdate;
    }; //shouldComponentUpdate

    // #endregion Life-Cycle Methods

    // #region " - - - - - - Data Access - - - - - - "

    fetchSeoReport = (keywords, url) => {
        // Generate the API URL.
        var params = {
            keywords: keywords === null ? null : encodeURIComponent(keywords),
            url: url === null ? null : encodeURIComponent(url)
        }
        let queryString = Object.keys(params).map(key => params[key] ? key + "=" + params[key] : '').filter(Boolean).join('&');
        let apiUrl = SeoReportSearch.SEO_REPORTS_URL_PATH + "?" + queryString;

        let responseStatusCode = 0;
        fetch(apiUrl)
            .then(response => {
                responseStatusCode = response.status;
                if (response.ok || ((responseStatusCode >= 400) && (responseStatusCode <= 500))) {
                    return response.json();
                }
                else {
                    throw new Error(response);
                }
            })
            .then(json => {
                if (responseStatusCode < 400) {
                    this.setState({
                        seoAnalysisReport: json,
                        shouldUpdate: true,
                        loading: false
                    });
                }
                else {
                    this.setState({
                        seoAnalysisReport: null,
                        errorResponse: json,
                        shouldUpdate: true,
                        loading: false
                    });
                }
            })
            .catch(error => console.log(error));
    }; //fetchSeoReport

    // #endregion Data Access

    // #region " - - - - - - Event Handlers - - - - - - "

    handleErrorAlertClose = () => {
        this.setState({
            errorResponse: null,
            shouldUpdate: true
        });
    }; //handleErrorAlertClose

    handleKeywordsInputChange = (e) => {
        this.setState({
            keywords: e.target.value,
            shouldUpdate: false
        });
    }; //handleKeywordsInputChange

    handleUrlInputChange = (e) => {
        this.setState({
            url: e.target.value,
            shouldUpdate: false
        });
    }; //handleUrlInputChange

    handleSearchButtonClick = (e) => {
        this.setState({
            shouldUpdate: true,
            loading: true,
            errorResponse: null
        });
        this.fetchSeoReport(this.state.keywords, this.state.url);
    }; //handleSearchButtonClick

    // #endregion Event Handlers

    // #region " - - - - - - Rendering Functionality - - - - - - "

    renderErrorAlert = () => {
        if (!this.state.errorResponse) return "";
        return (
            <Alert dismissible variant="danger" onClose={this.handleErrorAlertClose} >
                <Alert.Heading>Uhh Ohh! Something went wrong...</Alert.Heading>
                <p>
                    {this.state.errorResponse.message}
                </p>
            </Alert >
        );
    }; //renderErrorAlert

    renderResults = () => {
        // Return nothing if loading.
        if (this.state.loading) return null;

        // Attempt to retrieve the analysis report.
        let results = this.state.seoAnalysisReport === null
            ? null
            : this.state.seoAnalysisReport.keywordSearchResultsPositions;
        if (results === null) return results;

        // Format the results.
        results = results.length > 0
            ? results.join(", ")
            : "0";

        return (
            <div>
                <h4><b>Result Positions:</b> {results}</h4>
            </div>
        );
    }; //renderResults

    renderSearchFields = () => {
        let isLoading = this.state.loading;
        let buttonText = isLoading ? <em>Loading...</em> : <b>Search <FaSearch /></b>
        return (
            <Form>
                <Form.Group as={Col} md="12" controlId="keywordsInput">
                    <InputGroup>
                        <InputGroup.Prepend>
                            <InputGroup.Text id="keywordsInput"><strong>Keywords</strong></InputGroup.Text>
                        </InputGroup.Prepend>
                        <Form.Control
                            type="text"
                            placeholder="Enter your search terms here ..."
                            onChange={this.handleKeywordsInputChange}
                            aria-label="Search Keywords"
                            aria-describedby="keywordsInput"
                        />
                    </InputGroup>
                </Form.Group>
                <Form.Group as={Col} md="12" controlId="urlInput">
                    <InputGroup>
                        <InputGroup.Prepend>
                            <InputGroup.Text id="urlInput">&nbsp;&nbsp;&nbsp;<strong>Domain</strong></InputGroup.Text>
                        </InputGroup.Prepend>
                        <Form.Control
                            type="text"
                            placeholder="Your Domain (e.g. www.infotrack.com.au)"
                            onChange={this.handleUrlInputChange}
                            aria-label="Domain"
                            aria-describedby="urlInput"
                        />
                    </InputGroup>
                </Form.Group>
                <Form.Group as={Col} md="12" controlId="seachButton">
                    <Button
                        variant="outline-primary"
                        onClick={!isLoading ? this.handleSearchButtonClick : null}
                        disabled={isLoading}
                    >{buttonText}</Button>
                </Form.Group>
            </Form>
        );
    }; //renderSearchFields

    render() {
        let errorAlert = this.renderErrorAlert();
        let searchFields = this.renderSearchFields();
        let results = this.renderResults();
        return (
            <div style={{ margin: "10px 0px 0px 0px"}}>
                {errorAlert}
                <Card className="text-center">
                    <Card.Header>
                        <h3><em>Google Search - Result Positions</em></h3>
                    </Card.Header>
                    <Card.Body>
                        {searchFields}
                    </Card.Body>
                    <Card.Footer>
                        {results}
                    </Card.Footer>
                </Card>
            </div>
        );
    }; //render

    // #endregion Rendering Functionality
}