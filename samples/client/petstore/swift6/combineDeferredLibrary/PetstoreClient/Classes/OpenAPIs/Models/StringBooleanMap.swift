//
// StringBooleanMap.swift
//
// Generated by openapi-generator
// https://openapi-generator.tech
//

import Foundation

public struct StringBooleanMap: Sendable, Codable, QueryStringEncodable, Hashable {


    public enum CodingKeys: CodingKey, CaseIterable {
    }

    public var additionalProperties: [String: Bool] = [:]

    public subscript(key: String) -> Bool? {
        get {
            if let value = additionalProperties[key] {
                return value
            }
            return nil
        }

        set {
            additionalProperties[key] = newValue
        }
    }

    // Encodable protocol methods

    public func encode(to encoder: Encoder) throws {
        var container = encoder.container(keyedBy: CodingKeys.self)
        var additionalPropertiesContainer = encoder.container(keyedBy: String.self)
        try additionalPropertiesContainer.encodeMap(additionalProperties)
    }

    // Decodable protocol methods

    public init(from decoder: Decoder) throws {
        let container = try decoder.container(keyedBy: CodingKeys.self)

        var nonAdditionalPropertyKeys = Set<String>()
        let additionalPropertiesContainer = try decoder.container(keyedBy: String.self)
        additionalProperties = try additionalPropertiesContainer.decodeMap(Bool.self, excludedKeys: nonAdditionalPropertyKeys)
    }
}

