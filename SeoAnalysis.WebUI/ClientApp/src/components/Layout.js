import React, { Component } from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
        <Container>
            <Row style={{ marginBottom: "4em" }}>
                <NavMenu />
            </Row>
            <Row>
                <Col>
                    {this.props.children}
                </Col>
            </Row>
        </Container>
    );
  }
}
