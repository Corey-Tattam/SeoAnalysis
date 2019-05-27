import React, { Component } from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import { SeoReportSearch } from './seoReports/SeoReportSearch';

export class Home extends Component {
  static displayName = Home.name;

    render() {
        return (
          <div>
                <Container>
                    <Row>
                        <Col md="12">
                            <SeoReportSearch />
                        </Col>
                    </Row>
                </Container>
          </div>
        );
  }
}
