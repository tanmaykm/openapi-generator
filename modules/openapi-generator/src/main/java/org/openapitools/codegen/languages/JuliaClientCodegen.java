package org.openapitools.codegen.languages;

import org.openapitools.codegen.*;

import java.io.File;
import java.util.*;
import io.swagger.v3.oas.models.media.Schema;
import io.swagger.v3.oas.models.media.ArraySchema;
import io.swagger.v3.oas.models.parameters.Parameter;

import org.apache.commons.lang3.StringUtils;
import static org.openapitools.codegen.utils.StringUtils.camelize;
import org.openapitools.codegen.utils.ModelUtils;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class JuliaClientCodegen extends DefaultCodegen implements CodegenConfig {
    private final Logger LOGGER = LoggerFactory.getLogger(JuliaClientCodegen.class);

    protected String srcPath = "src";
    protected String apiSrcPath = "src/";
    protected String modelSrcPath = "src/";

    protected String apiDocPath = "docs/";
    protected String modelDocPath = "docs/";

    protected String packageName;
    
    /**
     * Configures the type of generator.
     *
     * @return the CodegenType for this generator
     * @see org.openapitools.codegen.CodegenType
     */
    public CodegenType getTag() {
        return CodegenType.CLIENT;
    }

    /**
     * Configures the name of the generator.
     * This will be used to refer to the generator when configuration is read from config files.
     *
     * @return the name of the generator
     */
    public String getName() {
        return "julia";
    }

    /**
     * Configures a help message for the generator.
     * TODO: add parameters, tips here
     *
     * @return the help message for the generator
     */
    public String getHelp() {
        return "Generates a julia client.";
    }

    public JuliaClientCodegen() {
        super();

        outputFolder = "generated-code" + File.separator + "julia";
        modelTemplateFiles.put("model.mustache", ".jl");
        apiTemplateFiles.put("api.mustache", ".jl");
        embeddedTemplateDir = templateDir = "julia";
        modelDocTemplateFiles.put("model_doc.mustache", ".md");
        apiDocTemplateFiles.put("api_doc.mustache", ".md");

        // apiPackage = "Apis";
        // modelPackage = "Models";
        supportingFiles.clear();
        supportingFiles.add(new SupportingFile("README.mustache", "README.md"));

        reservedWords = new HashSet<String> (
            Arrays.asList(
                "if", "else", "elseif", "while", "for", "begin", "end", "quote",
                "try", "catch", "return", "local", "function", "macro", "ccall", "finally", "break", "continue",
                "global", "module", "using", "import", "export", "const", "let", "do", "baremodule",
                "Type", "Enum", "Any", "DataType", "Base"
            )
        );

        // Language Specific Primitives.  These types will not trigger imports by the client generator
        languageSpecificPrimitives = new HashSet<String>(
            Arrays.asList("Int128", "Int64", "Int32", "Int16", "Int8", "UInt128", "UInt64", "UInt32", "UInt16", "UInt8", "Float64", "Float32", "Float16", "Char", "Vector", "Array", "Bool", "String", "Integer", "Nothing")
        );

        typeMapping.clear();
        typeMapping.put("integer", "Int32");
        typeMapping.put("long", "Int64");
        typeMapping.put("float", "Float32");
        typeMapping.put("double", "Float64");
        typeMapping.put("string", "String");
        typeMapping.put("byte", "UInt8");
        typeMapping.put("binary", "Vector{UInt8}");
        typeMapping.put("boolean", "Bool");
        typeMapping.put("number", "Float32");
        typeMapping.put("array", "Vector");
        typeMapping.put("map", "Dict");
        typeMapping.put("date", "Date");
        typeMapping.put("Object", "Any");
        typeMapping.put("DateTime", "ZonedDateTime");
        typeMapping.put("File", "String");
        typeMapping.put("UUID", "String");
        typeMapping.put("ByteArray", "Vector{UInt8}");

        cliOptions.clear();
        cliOptions.add(new CliOption(CodegenConstants.PACKAGE_NAME, "Julia package name.").defaultValue("APIClient"));
    }

    public void setPackageName(String packageName) {
        this.packageName = packageName;
    }

    private static String dropDots(String str) {
        return str.replaceAll("\\.", "_");
    }

    @Override
    public void processOpts() {
        super.processOpts();
        if (additionalProperties.containsKey(CodegenConstants.PACKAGE_NAME)) {
            setPackageName((String) additionalProperties.get(CodegenConstants.PACKAGE_NAME));
        }
        else {
            setPackageName("APIClient");
            additionalProperties.put(CodegenConstants.PACKAGE_NAME, packageName);
        }
        additionalProperties.put("apiDocPath", apiDocPath);
        additionalProperties.put("modelDocPath", modelDocPath);
        supportingFiles.add(new SupportingFile("client.mustache", srcPath, packageName + ".jl"));
        supportingFiles.add(new SupportingFile("modelincludes.mustache", srcPath, "modelincludes.jl"));
    }

    /**
     * Escapes a reserved word as defined in the `reservedWords` array. Handle escaping
     * those terms here.  This logic is only called if a variable matches the reseved words
     * 
     * @return the escaped term
     */
    @Override
    public String escapeReservedWord(String name) {
        if (reservedWords.contains(name)) {
            return "__" + name + "__";  // add underscores to reserved words, and also to obscure it to lessen chances of clashing with any other names
        } else {
            return name;
        }
    }

    /**
     * Location to write model files.
     */
    @Override
    public String modelFileFolder() {
        return (outputFolder + "/" + modelSrcPath).replace('/', File.separatorChar);
    }

    /**
     * Location to write api files.
     */
    @Override
    public String apiFileFolder() {
        return (outputFolder + "/" + apiSrcPath).replace('/', File.separatorChar);
    }

    @Override
    public String apiDocFileFolder() {
        return (outputFolder + "/" + apiDocPath).replace('/', File.separatorChar);
    }

    @Override
    public String modelDocFileFolder() {
        return (outputFolder + "/" + modelDocPath).replace('/', File.separatorChar);
    }

    @Override
    public String toModelFilename(String name) {
        name = sanitizeName(name);
        name = name.replaceAll("$", "");
        return "model_" + camelize(dropDots(name));
    }

    @Override
    public String toApiFilename(String name) {
        name = name.replaceAll("-", "_");
        return "api_" + camelize(name) + "Api";
    }

    @Override
    public String toApiName(String name) {
        if (name.length() == 0) {
            return "DefaultApi";
        }
        // e.g. phone_number_api => PhoneNumberApi
        return camelize(name) + "Api";
    }

    @Override
    public String toParamName(String name) {
        return escapeReservedWord(sanitizeName(name));
    }

    @Override
    public String toApiVarName(String name) {
        return escapeReservedWord(sanitizeName(name));
    }

    @Override
    public String toVarName(String name) {
        return escapeReservedWord(sanitizeName(name));
    }

    /**
     * Sanitize name (parameter, property, method, etc)
     *
     * @param name string to be sanitize
     * @return sanitized string
     */
    @Override
    @SuppressWarnings("static-method")
    public String sanitizeName(String name) {
        if (name == null) {
            LOGGER.error("String to be sanitized is null. Default to ERROR_UNKNOWN");
            return "ERROR_UNKNOWN";
        }

        // if the name is just '$', map it to 'value' for the time being.
        if ("$".equals(name)) {
            return "value";
        }

        name = name.replaceAll("\\[\\]", "");
        name = name.replaceAll("\\[", "_");
        name = name.replaceAll("\\]", "");
        name = name.replaceAll("\\(", "_");
        name = name.replaceAll("\\)", "");
        name = name.replaceAll("\\.", "_");
        name = name.replaceAll("-", "_");
        name = name.replaceAll(" ", "_");
        return name.replaceAll("[^a-zA-Z0-9_{}]", "");
    }

    /**
     * Output the proper Julia model name.
     *
     * @param name the name of the model
     * @return Julia model name
     */
    @Override
    public String toModelName(final String name) {
        String result = sanitizeName(name);

        // remove dollar sign
        result = result.replaceAll("$", "");

        // model name cannot use reserved keyword, e.g. return
        if (isReservedWord(result)) {
            LOGGER.warn(result + " (reserved word) cannot be used as model name. Renamed to " + camelize("model_" + result));
            result = "model_" + result; // e.g. return => ModelReturn (after camelize)
        }

        // model name starts with number
        if (result.matches("^\\d.*")) {
            LOGGER.warn(result + " (model name starts with number) cannot be used as model name. Renamed to " + camelize("model_" + result));
            result = "model_" + result; // e.g. 200Response => Model200Response (after camelize)
        }

        if (!StringUtils.isEmpty(modelNamePrefix)) {
            result = modelNamePrefix + "_" + result;
        }

        if (!StringUtils.isEmpty(modelNameSuffix)) {
            result = result + "_" + modelNameSuffix;
        }

        // camelize the model name
        // phone_number => PhoneNumber
        result = camelize(result);

        return result;
    }

    @Override
    public String getTypeDeclaration(Schema p) {
        if (ModelUtils.isArraySchema(p)) {
            ArraySchema ap = (ArraySchema) p;
            Schema inner = ap.getItems();
            return getSchemaType(p) + "{" + getTypeDeclaration(inner) + "}";
        } else if (ModelUtils.isMapSchema(p)) {
            Schema inner = getAdditionalProperties(p);
            return getSchemaType(p) + "{String, " + getTypeDeclaration(inner) + "}";
        }
        return super.getTypeDeclaration(p);
    }    


    /**
     * Return the type declaration for a given schema
     *
     * @param schema the schema
     * @return the type declaration
     */
    @Override
    public String getSchemaType(Schema p) {
        String openAPIType = super.getSchemaType(p);
        String type = null;

        if (openAPIType == null) {
            LOGGER.error("OpenAPI Type for {} is null. Default to Object instead.", p.getName());
            openAPIType = "Object";
        }

        if (typeMapping.containsKey(openAPIType)) {
            type = typeMapping.get(openAPIType);
            if(languageSpecificPrimitives.contains(type)) {
                return type;
            }
        } else {
            type = openAPIType;
        }

        return toModelName(type);
    }

    /**
     * Return the default value of the property
     * @param p OpenAPI property object
     * @return string presentation of the default value of the property
     */
    @Override
    public String toDefaultValue(Schema p) {
        if (ModelUtils.isBooleanSchema(p)) {
            if (p.getDefault() != null) {
                return p.getDefault().toString();
            }
        } else if (ModelUtils.isDateSchema(p)) {
            // TODO
        } else if (ModelUtils.isDateTimeSchema(p)) {
            // TODO
        } else if (ModelUtils.isIntegerSchema(p) || ModelUtils.isLongSchema(p) || ModelUtils.isNumberSchema(p)) {
            if (p.getDefault() != null) {
                return p.getDefault().toString();
            }
        } else if (ModelUtils.isStringSchema(p)) {
            if (p.getDefault() != null) {
                String _default = (String) p.getDefault();
                if (p.getEnum() == null) {
                    return "\"" + _default + "\"";
                } else {
                    // convert to enum var name later in postProcessModels
                    return _default;
                }
            }
        }

        return "nothing";
    }

    @Override
    public String escapeUnsafeCharacters(String input) {
        return input;
    }

    /**
     * Escape single and/or double quote to avoid code injection 
     * @param input String to be cleaned up
     * @return string with quotation mark removed or escaped
     */
    public String escapeQuotationMark(String input) {
        return input.replace("\"", "\\\"");
    }

    private String escapeBaseName(String name) {
        name = name.replaceAll("\\$", "\\\\\\$");
        return name;
    }

    /**
     * Convert OpenAPI Parameter object to Codegen Parameter object
     *
     * @param imports set of imports for library/package/module
     * @param param   OpenAPI parameter object
     * @return Codegen Parameter object
     */
    @Override
    public CodegenParameter fromParameter(Parameter param, Set<String> imports) {
        CodegenParameter parameter = super.fromParameter(param, imports);
        parameter.baseName = escapeBaseName(parameter.baseName);
        return parameter;
    }

    /**
     * Convert OAS Property object to Codegen Property object.
     * <p>
     * The return value is cached. An internal cache is looked up to determine
     * if the CodegenProperty return value has already been instantiated for
     * the (String name, Schema p) arguments.
     * Any subsequent processing of the CodegenModel return value must be idempotent
     * for a given (String name, Schema schema).
     *
     * @param name     name of the property
     * @param p        OAS property schema
     * @param required true if the property is required in the next higher object schema, false otherwise
     * @return Codegen Property object
     */    
    @Override
    public CodegenProperty fromProperty(String name, Schema p, boolean required) {
        CodegenProperty property = super.fromProperty(name, p, required);
        property.baseName = escapeBaseName(property.baseName);
        return property;
    }
}